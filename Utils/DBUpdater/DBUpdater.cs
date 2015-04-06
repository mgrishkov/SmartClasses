using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;

namespace SmartClasses.Utils.DBUpdater
{
    public class DBUpdater
    {
        public class BeforePackInstallEventArgs : EventArgs
        {
            public IPack Pack { get; set; }
            public bool Cancel { get; set; }
        }
        public class AfterPackInstallEventArgs : EventArgs
        {
            public IPack Pack { get; set; }
        }

        public EventHandler<BeforePackInstallEventArgs> BeforePackInstall;
        public EventHandler<AfterPackInstallEventArgs> AfterPackInstall;

        private List<IPack> _packs;

        public IEnumerable<IPack> Packs
        {
            get { return _packs; }
        }

        public Settings Settings { get; private set; }
        public Version OldVersion { get; private set; }
        public Version NewVersion { get; private set; }
        public StringBuilder Log { get; private set; }

        public DBUpdater(string connectionString,
                         string dataBase,
                         string serviceSchema,
                         string packFolder)
        {
            Log = new StringBuilder();
            _packs = new List<IPack>();

            Settings.Database = dataBase;
            Settings.ConnectionString = connectionString;
            Settings.ServiceSchema = serviceSchema;
            Settings.PacksFolder = packFolder;

            CreateDatabase();
            CreateSchema();
            GetCurrentVersion();
            LoadPacks();
        }

        public DBUpdater(string connectionString,
                         string dataBase,
                         string serviceSchema,
                         ResourceManager packs)
        {
            Log = new StringBuilder();
            _packs = new List<IPack>();

            Settings.Database = dataBase;
            Settings.ConnectionString = connectionString;
            Settings.ServiceSchema = serviceSchema;
            Settings.PacksResource = packs;

            CreateDatabase();
            CreateSchema();
            GetCurrentVersion();
            LoadPacks();
        }

        public void InstallUpdates()
        {
            var toInstall = Packs
                .Where(x => x.Version > OldVersion)
                .OrderBy(x => x.Version);

            foreach (var itm in toInstall)
            {
                Log.AppendFormat("Installing pack {0}...{1}", itm.Version, Environment.NewLine);
                itm.Load();

                var args = new BeforePackInstallEventArgs() { Pack = itm };
                OnBeforeInstallPack(args);
                
                if (!args.Cancel)
                {
                    itm.Install();
                    Log.Append(itm.Log);
                    OnAfterInstallPack(itm);
                };

                Log.AppendFormat("Pack {0} has been installed{1}{1}", itm.Version, Environment.NewLine);
            };
        }

        public bool IsNewVersionFound
        {
            get { return OldVersion < NewVersion; }
        }

        private void LoadPacks()
        {
            Version version = null;

            if (!String.IsNullOrWhiteSpace(Settings.PacksFolder))
            {
                var files = Directory.GetFiles(Settings.PacksFolder)
                    .Where(x => x.EndsWith(".sql"));
                foreach (var itm in files)
                {        
                    var file = new FileInfo(itm);
                    var name = Path.GetFileNameWithoutExtension(itm);

                    if (Version.TryParse(name, out version))
                    {
                        var pack = new FilePack()
                        {
                            Version = version,
                            File = file
                        };
                        _packs.Add(pack);
                    };
                };
            }
            else if(Settings.PacksResource != null)
            {
                var resources = Settings.PacksResource.GetResourceSet(CultureInfo.CurrentCulture, true, true);

                var id = resources.GetEnumerator();

                while(id.MoveNext())
                {
                    var strVersion = id.Key.ToString().Substring(2).Replace('_', '.');

                    if (Version.TryParse(strVersion, out version))
                    {
                        var pack = new ResourcePack()
                        {
                            Version = version
                        };
                        _packs.Add(pack);
                    };
                }
            }
            NewVersion = _packs.Max(x => x.Version);
        }

        private void CreateSchema()
        {
            var script = String.Format(
                @"if(schema_id('{0}') is null)
                  begin 
                    exec ('create schema [{0}]');
                  end;

                 if(object_id('[{0}].[DBVersion]') is null)
                 begin
                     create table [{0}].[DBVersion]
                     ( 
                         [ID] int identity,
                         [Major] int not null, 
                         [Minor] int not null,
                         [Build] int not null,
                         [Revision] int not null,
                         [InstallationStarted] datetime2 not null,
                         [InstallationFinished] datetime2,
                         constraint PK#DBVersion primary key ([ID]),
                         constraint UK#DBVersion unique ([Major], [Minor], [Build], [Revision])
                     );
                 end",
                Settings.ServiceSchema);

            using (var conn = new SqlConnection(Settings.ConnectionString))
            using (var cmd = new SqlCommand(Regex.Replace(script, @"\s+", " "), conn))
            {
                conn.Open();
                conn.ChangeDatabase(Settings.Database);
                cmd.ExecuteNonQuery();
            };
        }
        private void CreateDatabase()
        {
            var script = String.Format(
                @"if(db_id('{0}') is null)
                  begin 
                    exec ('CREATE DATABASE [{0}]');
                  end;",
                Settings.Database);

            using (var conn = new SqlConnection(Settings.ConnectionString))
            using (var cmd = new SqlCommand(Regex.Replace(script, @"\s+", " "), conn))
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            };
        }

        private void GetCurrentVersion()
        {
            var script = String.Format(@"select top 1 [Major], [Minor], [Build], [Revision] from [{0}].[DBVersion] where [InstallationFinished] is not null order by [Major] desc, [Minor] desc, [Build] desc, [Revision] desc",
                Settings.ServiceSchema);
            try
            {
                using (var conn = new SqlConnection(Settings.ConnectionString))
                using (var cmd = new SqlCommand(script, conn))
                {
                    conn.Open();
                    conn.ChangeDatabase(Settings.Database);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var major = reader.GetFieldValue<int>(0);
                            var minor = reader.GetFieldValue<int>(1);
                            var build = reader.GetFieldValue<int>(2);
                            var revision = reader.GetFieldValue<int>(3);

                            OldVersion = new Version(major, minor, build, revision);
                        }
                        else
                        {
                            OldVersion = new Version();
                        }
                    }
                }
            }
            catch
            {
                OldVersion = new Version();
            }
        }
        public virtual void OnBeforeInstallPack(BeforePackInstallEventArgs args)
        {
            if (BeforePackInstall != null)
            {
                BeforePackInstall(this, args);
            };

            if (!args.Cancel)
            {
                var script = String.Format(@"if(not exists (select 1
                                                              from [{0}].[DBVersion]
                                                             where [Major] = @major
                                                               and [Minor] = @minor 
                                                               and [Build] = @build
                                                               and [Revision] = @revision))
                                             begin 
                                                insert into [{0}].[DBVersion] 
                                                    ([Major], [Minor], [Build], [Revision], [InstallationStarted]) 
                                                values 
                                                    (@major, @minor, @build, @revision, getdate());
                                             end;", 
                                Settings.ServiceSchema);

                using (var conn = new SqlConnection(Settings.ConnectionString))
                using (var cmd = new SqlCommand(Regex.Replace(script, @"\s+", " "), conn))
                {
                    conn.Open();
                    conn.ChangeDatabase(Settings.Database);

                    var pack = args.Pack;

                    cmd.Parameters.Add(new SqlParameter("@major", pack.Version.Major));
                    cmd.Parameters.Add(new SqlParameter("@minor", pack.Version.Minor));
                    cmd.Parameters.Add(new SqlParameter("@build", pack.Version.Build));
                    cmd.Parameters.Add(new SqlParameter("@revision", pack.Version.Revision));

                    cmd.ExecuteNonQuery();
                };
            };

        }
        public virtual void OnAfterInstallPack(IPack pack)
        {
            var script = String.Format(@"update [{0}].[DBVersion] 
                                            set [InstallationFinished] = getdate() 
                                          where [Major] = @major
                                            and [Minor] = @minor
                                            and [Build] = @build
                                            and [Revision] = @revision", Settings.ServiceSchema);
            using (var conn = new SqlConnection(Settings.ConnectionString))
            using (var cmd = new SqlCommand(Regex.Replace(script, @"\s+", " "), conn))
            {
                conn.Open();
                conn.ChangeDatabase(Settings.Database);

                cmd.Parameters.Add(new SqlParameter("@major", pack.Version.Major));
                cmd.Parameters.Add(new SqlParameter("@minor", pack.Version.Minor));
                cmd.Parameters.Add(new SqlParameter("@build", pack.Version.Build));
                cmd.Parameters.Add(new SqlParameter("@revision", pack.Version.Revision));

                cmd.ExecuteNonQuery();
            };

            var args = new AfterPackInstallEventArgs() { Pack = pack };
            if (AfterPackInstall != null)
            {
                AfterPackInstall(this, args);
            };
        }
    }
}

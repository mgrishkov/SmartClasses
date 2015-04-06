using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClasses.Utils.DBUpdater
{
    public class BasePack
    {
        public Version Version { get; set; }
        public string Script { get; set; }
        public StringBuilder Log { get; set; }

        public BasePack()
        {
            Log = new StringBuilder();
        }

        public virtual void Install()
        {
            if (String.IsNullOrWhiteSpace(Settings.ConnectionString))
                throw new ArgumentException("Settings.Connectionstring is Empty");

            if (String.IsNullOrWhiteSpace(Script))
                throw new ArgumentException("Pack has not been loaded yet");

            using (var conn = new SqlConnection(Settings.ConnectionString))
            {
                conn.Open();
                conn.InfoMessage += readInfoMessage;

                var commands = Script.Split(new string[] { "GO", "go", "Go", "gO" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var sql in commands)
                {
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    };
                }
            }

        }
        protected virtual void readInfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            Log.AppendFormat("{0:dd.MM.yyyy HH:mm:ss.ms} - Info {1}{2}", DateTime.Now, e.Message, Environment.NewLine);
            //Log.AppendFormat("{0:dd.MM.yyyy HH:mm:ss.ms} - Error Source: {1}, Errors: {2}", DateTime.Now, e.Source, e.Errors);
        }
    }
}

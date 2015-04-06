using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Resources;

namespace SmartClasses.Utils.DBUpdater
{
    public class ResourcePack : BasePack, IPack
    {
        public ResourcePack() : base()
        {
        }

        public void Load()
        {
            var version = String.Format("v_{0}_{1}_{2}_{3}", Version.Major, Version.Minor, Version.Build, Version.Revision);
            Script = Settings.PacksResource.GetString(version);
        }
    }
}

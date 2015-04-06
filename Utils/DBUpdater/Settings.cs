using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace SmartClasses.Utils.DBUpdater
{
    public class Settings
    {
        public static string PacksFolder { get; internal set; }
        public static ResourceManager PacksResource { get; internal set; }
        public static string ConnectionString { get; internal set; }
        public static string ServiceSchema { get; internal set; }
        public static string Database { get; internal set; }
    }
}

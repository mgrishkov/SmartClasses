using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SmartClasses.Utils.DBUpdater
{
    public class FilePack : BasePack, IPack
    {
        public FileInfo File { get; set; }

        public FilePack() : base()
        {
        }

        public void Load()
        {
            using (var stream = File.OpenText())
            {
                Script = stream.ReadToEnd();
            };
        }
    }
}

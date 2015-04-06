using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClasses.Utils.DBUpdater
{
    public interface IPack
    {
        Version Version { get; set; }
        string Script { get; set; }
        StringBuilder Log { get; set; }

        void Load();

        void Install();
    }
}

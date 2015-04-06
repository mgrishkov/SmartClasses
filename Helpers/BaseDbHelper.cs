using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClasses.Helpers
{
    public class BaseDbHelper
    {
        protected const int COMMAND_TIMEOUT = 4 * 60 * 60; // seconds

        public static string ConnectionString { get; set; }
    }
}

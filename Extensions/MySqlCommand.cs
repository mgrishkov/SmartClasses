using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SmartClasses.Extensions
{
    public class ExtensionMethods
    {
        public static MySqlCommand Proc(this MySqlConnection connection, string proc)
        {
            MySqlCommand command = new MySqlCommand(proc, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            return command;
        }
        public static MySqlCommand Command(this MySqlConnection connection, string sql)
        {
            MySqlCommand command = new MySqlCommand(sql, connection);
            return command;
        }
        public static MySqlCommand AddParameter(this MySqlCommand command, string parameterName, object value)
        {
            command.Parameters.AddWithValue(parameterName, (object)value ?? (object)DBNull.Value);
            return command;
        }
        public static MySqlCommand AddReturnParameter(this MySqlCommand command, string parameterName)
        {
            MySqlParameter param = new MySqlParameter();
            param.ParameterName = parameterName;
            param.Direction = System.Data.ParameterDirection.InputOutput;
            command.Parameters.Add(param);
            return command;
        }
    }
}

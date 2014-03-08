using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;

namespace SmartClasses.Extensions
{
    public partial class ExtensionMethods
    {
        public static MySqlCommand Proc(this MySqlConnection connection, string proc)
        {
            var command = new MySqlCommand(proc, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            return command;
        }
        public static MySqlCommand Command(this MySqlConnection connection, string sql)
        {
            var command = new MySqlCommand(sql, connection);
            return command;
        }
        public static MySqlCommand AddParameter(this MySqlCommand command, string parameterName, object value)
        {
            command.Parameters.AddWithValue(parameterName, (object)value ?? (object)DBNull.Value);
            return command;
        }
        public static MySqlCommand AddReturnParameter(this MySqlCommand command, string parameterName)
        {
            var param = new MySqlParameter();
            param.ParameterName = parameterName;
            param.Direction = System.Data.ParameterDirection.InputOutput;
            command.Parameters.Add(param);
            return command;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading;
using System.Web;
using SmartClasses.Interfaces;

namespace SmartClasses.Helpers
{
    public class SqlDbHelper : BaseDbHelper
    {

        #region Parameters

        protected static SqlParameter GetParameter<T>(string name, T? val) where T : struct
        {
            var parameter = new SqlParameter {ParameterName = name};

            if (val.HasValue)
                parameter.Value = val.Value;
            else
            {
                // small hack to define SqlParameter.SqlDbType
                parameter.Value = default(T);
                var type = parameter.SqlDbType;

                parameter.Value = DBNull.Value;
                parameter.SqlDbType = type;
            }

            return parameter;
        }

        protected static SqlParameter GetParameter(string name, string val)
        {
            return new SqlParameter {ParameterName = name, Value = string.IsNullOrEmpty(val) || val.Trim().Length == 0 ? (object)DBNull.Value : val.Trim()};
        }

        // Due to the fact that we use SAP boolean representation, where ' ' (space) is a false, and 'X' is a true, 
        // we cannot afford trimming our strings right and left.
        protected static SqlParameter GetParameter(string name, string val, bool allowWhiteSpaces)
        {
            if(!allowWhiteSpaces)
                return new SqlParameter { ParameterName = name, Value = string.IsNullOrEmpty(val) || val.Trim().Length == 0 ? (object)DBNull.Value : val.Trim() };
            else
                return new SqlParameter { ParameterName = name, Value = string.IsNullOrEmpty(val) ? (object)DBNull.Value : val };
        }

        protected static SqlParameter GetParameter<T>(string name, T val, ParameterDirection direction = ParameterDirection.Input) where T : struct
        {
            return new SqlParameter(name, val) {Direction = direction};
        }

        protected static SqlParameter GetParameter(string name, SqlDbType type, ParameterDirection direction = ParameterDirection.Output)
        {
            switch(type)
            {
                case SqlDbType.NVarChar:
                    return new SqlParameter(name, type) { Direction = direction, Size = 8000 };
                default:
                    return new SqlParameter(name, type) { Direction = direction };
            }
            
        }


        protected static SqlParameter GetParameter(string name, byte[] val)
        {
            return new SqlParameter {ParameterName = name, SqlDbType = SqlDbType.Image, Value = val == null || val.Length == 0 ? (object)DBNull.Value : val};
        }

        #endregion

        #region Connections

        static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        #endregion

        #region Command

        protected static SqlCommand GetCommand(String text, CommandType commandType, params SqlParameter[] parameters)
        {
            var connection = GetConnection();

            var command = new SqlCommand(text, connection) {CommandType = commandType, CommandTimeout = COMMAND_TIMEOUT};

            if (commandType == CommandType.StoredProcedure && parameters != null && parameters.Length > 0)
                command.Parameters.AddRange(parameters);

            return command;
        }

        protected static int ExecuteNonQuery(string text, params SqlParameter[] parameters)
        {
            using (var command = GetCommand(text, CommandType.StoredProcedure, parameters))
            {
                if (command.Connection.State == ConnectionState.Open)
                    return command.ExecuteNonQuery();

                using (command.Connection)
                {
                    command.Connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }

        protected static T ExecuteScalar<T>(string text, T defVal, params SqlParameter[] parameters)
        {
            using (var command = GetCommand(text, CommandType.Text, parameters))
            {
                Object returnValue;

                // command executes in the scope of transaction
                if (command.Connection.State == ConnectionState.Open)
                    returnValue = command.ExecuteScalar();
                else
                {
                    using (command.Connection)
                    {
                        command.Connection.Open();
                        returnValue = command.ExecuteScalar();
                    }
                }

                return returnValue == null || returnValue == DBNull.Value ? defVal : (T)returnValue;
            }
        }

        /// <summary>
        /// Get opened SqlDataReader
        /// </summary>
        /// <param name="text">Store procedure name</param>
        /// <param name="commandType">Command type</param>
        /// <param name="parameters">Stored procedure parameters</param>
        /// <returns>Opened SqlDataReader</returns>
        protected static SqlDataReader GetReader(string text, CommandType commandType, params SqlParameter[] parameters)
        {
            var command = GetCommand(text, commandType, parameters);

            var behavior = CommandBehavior.Default;

            if (command.Connection.State == ConnectionState.Open)
                return command.ExecuteReader(behavior);

            command.Connection.Open();
            
            behavior |= CommandBehavior.CloseConnection;

            return command.ExecuteReader(behavior);
        }

        #endregion

        #region Entity

        protected static T Get<T>(DbDataReader reader) where T : IDatabaseEntity, new()
        {
            var obj = new T();
            if (!reader.HasRows)
                return default(T);

            obj.Fill(reader);

            return obj;
        }

        /// <summary>
        /// Get the first record from SqlDataReader
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="text">Stored procedure name</param>
        /// <param name="commandType">Command type</param>
        /// <param name="parameters">Stored procedure parameters</param>
        /// <returns></returns>
        protected static T Get<T>(String text, CommandType commandType, params SqlParameter[] parameters) where T : IDatabaseEntity, new()
        {
            using (var reader = GetReader(text, commandType, parameters))
            {
                reader.Read();

                return Get<T>(reader);
            }
        }

        protected static List<T> GetList<T>(String text, CommandType commandType, params SqlParameter[] parameters) where T : IDatabaseEntity, new()
        {
            using (var reader = GetReader(text, commandType, parameters))
            {
                return GetList<T>(reader);
            }
        }

        protected static List<T> GetList<T>(DbDataReader reader) where T : IDatabaseEntity, new()
        {
            var list = new List<T>();

            while (reader.Read())
            {
                list.Add(Get<T>(reader));
            }

            reader.Close();

            return list;
        }

        #endregion
    }
}
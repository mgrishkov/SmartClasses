using System;
using System.Data.Common;
using System.Runtime.CompilerServices;

namespace SmartClasses.Entities
{
    public abstract class BaseEntity
    {
        public virtual dynamic Get<T>(DbDataReader r, String columnName)
        {
            if (typeof(T) == typeof(Int32))
                return Convert.ToInt32(r[columnName]);

            if (typeof(T) == typeof(Int32?))
                return r[columnName] != DBNull.Value ? Convert.ToInt32(r[columnName]) : (Int32?)null;

            if (typeof(T) == typeof(Int64))
                return (Int64)r[columnName];

            if (typeof(T) == typeof(Int64?))
                return r[columnName] != DBNull.Value ? Convert.ToInt64(r[columnName]) : (Int64?)null;

            if (typeof(T) == typeof(String))
                return r[columnName] != DBNull.Value ? r[columnName] : null;

            if (typeof(T) == typeof(DateTime))
                return (DateTime)r[columnName];

            if (typeof(T) == typeof(DateTime?))
                return r[columnName] != DBNull.Value ? (DateTime)r[columnName] : (DateTime?)null;

            if (typeof(T) == typeof(TimeSpan))
                return (TimeSpan)r[columnName];

            if (typeof(T) == typeof(TimeSpan?))
                return r[columnName] != DBNull.Value ? (TimeSpan)r[columnName] : (TimeSpan?)null;

            if (typeof(T) == typeof(Boolean))
                return Convert.ToBoolean(r[columnName]);

            if (typeof(T) == typeof(Boolean?))
                return r[columnName] != DBNull.Value ? Convert.ToBoolean(r[columnName]) : (Boolean?)null;

            if (typeof(T) == typeof(Decimal))
                return Convert.ToDecimal(r[columnName]);

            if (typeof(T) == typeof(Decimal?))
                return r[columnName] != DBNull.Value ? Convert.ToDecimal(r[columnName]) : (Decimal?)null;

            if (typeof(T) == typeof(TimeSpan))
                return (TimeSpan)r[columnName];

            if (typeof(T) == typeof(TimeSpan?))
                return r[columnName] != DBNull.Value ? (TimeSpan)r[columnName] : (TimeSpan?)null;

            throw new Exception(String.Format("Type [{0}] is not supported.", typeof(T).FullName));
           
        }


    }
}

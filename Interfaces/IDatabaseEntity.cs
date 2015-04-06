using System.Data.Common;

namespace SmartClasses.Interfaces
{
    public interface IDatabaseEntity
    {
        void Fill(DbDataReader r);
    }
}

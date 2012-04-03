using Simple.Data;

namespace GiveCRM.Admin.DataAccess.Test
{
    class SimpleDataFileDatabaseProvider : IDatabaseProvider
    {
        public dynamic GetDatabase()
        {
            return Database.OpenFile("TestDB.sdf");
        }
    }
}

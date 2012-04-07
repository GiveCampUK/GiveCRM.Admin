using System;
using Simple.Data;

namespace GiveCRM.Admin.DataAccess
{
    public class SimpeDataNamedConnectionDatabaseProvider : IDatabaseProvider
    {
        private readonly string namedConnection;

        public SimpeDataNamedConnectionDatabaseProvider(string namedConnection)
        {
            if (namedConnection == null)
            {
                throw new ArgumentNullException("namedConnection");
            }

            this.namedConnection = namedConnection;
        }

        public dynamic GetDatabase()
        {
            return Database.OpenNamedConnection(this.namedConnection);
        }
    }
}
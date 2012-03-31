using GiveCRM.Admin.BusinessLogic;
using Simple.Data;

namespace GiveCRM.Admin.DataAccess
{
    public abstract class RepositoryFactoryBase : IRepositoryFactory
    {
        public abstract IRepository<T> CreateRepository<T>(string namedConnection);

        protected dynamic CreateSimpleDataConnection(string namedConnection)
        {
            return Database.OpenNamedConnection(namedConnection);
        }
    }
}

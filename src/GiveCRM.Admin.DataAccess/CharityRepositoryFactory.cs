using GiveCRM.Admin.BusinessLogic;

namespace GiveCRM.Admin.DataAccess
{
    public class CharityRepositoryFactory : RepositoryFactoryBase
    {
        public override IRepository<T> CreateRepository<T>(string namedConnection)
        {
            return (IRepository<T>) new Charities(CreateSimpleDataConnection(namedConnection));
        }
    }
}
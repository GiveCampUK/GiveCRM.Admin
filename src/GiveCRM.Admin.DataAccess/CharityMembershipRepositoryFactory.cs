using GiveCRM.Admin.BusinessLogic;

namespace GiveCRM.Admin.DataAccess
{
    public class CharityMembershipRepositoryFactory : RepositoryFactoryBase
    {
        public override IRepository<T> CreateRepository<T>(string namedConnection)
        {
            return (IRepository<T>) new CharitiesMemberships(CreateSimpleDataConnection(namedConnection));
        }
    }
}
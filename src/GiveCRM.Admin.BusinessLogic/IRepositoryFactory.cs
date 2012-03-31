namespace GiveCRM.Admin.BusinessLogic
{
    public interface IRepositoryFactory
    {
        IRepository<T> CreateRepository<T>(string namedConnection);
    }
}

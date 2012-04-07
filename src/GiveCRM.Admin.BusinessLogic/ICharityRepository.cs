using GiveCRM.Admin.Models;

namespace GiveCRM.Admin.BusinessLogic
{
    public interface ICharityRepository : IRepository<Charity>
    {
        Charity GetByUserName(string username);
    }
}
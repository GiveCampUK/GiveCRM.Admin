using GiveCRM.Admin.Models;

namespace GiveCRM.Admin.BusinessLogic
{
    public interface ICharitiesMembershipRepository : IRepository<CharityMembership>
    {
        CharityMembership GetByUserId(string userId);
    }
}
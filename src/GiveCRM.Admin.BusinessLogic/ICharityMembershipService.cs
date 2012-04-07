using GiveCRM.Admin.Models;

namespace GiveCRM.Admin.BusinessLogic
{
    public interface ICharityMembershipService
    {
        bool RegisterCharityWithUser(RegistrationInfo registrationInfo, User user);
    }
}

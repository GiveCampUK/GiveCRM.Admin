using GiveCRM.Admin.Models;

namespace GiveCRM.Admin.BusinessLogic
{
    public interface ICharityMembershipService
    {
        bool RegisterUserAndCharity(RegistrationInfo registrationInfo);
    }
}

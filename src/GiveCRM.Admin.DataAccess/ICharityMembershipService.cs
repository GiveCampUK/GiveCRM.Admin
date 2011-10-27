using GiveCRM.Admin.Models;

namespace GiveCRM.Admin.DataAccess
{
    public interface ICharityMembershipService
    {
        bool RegisterUserAndCharity(RegistrationInfo registrationInfo);
    }
}

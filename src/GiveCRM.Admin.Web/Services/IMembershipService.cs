namespace GiveCRM.Admin.Web.Services
{
    public interface IMembershipService
    {
        IMember GetUser(string username);
        MembershipCreateStatus CreateUser(string username, string password, string email);
    }
}

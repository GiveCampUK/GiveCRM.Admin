namespace GiveCRM.Admin.Web.Services
{
    public enum MembershipCreateStatus
    {
        Success,
        DuplicateEmail,
        DuplicateUsername,
        InvalidEmail,
        InvalidUsername,
        InvalidPassword
    }
}
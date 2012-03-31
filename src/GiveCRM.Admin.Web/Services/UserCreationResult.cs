namespace GiveCRM.Admin.Web.Services
{
    public enum UserCreationResult
    {
        Success,
        DuplicateEmail,
        DuplicateUsername,
        InvalidEmail,
        InvalidUsername,
        InvalidPassword
    }
}
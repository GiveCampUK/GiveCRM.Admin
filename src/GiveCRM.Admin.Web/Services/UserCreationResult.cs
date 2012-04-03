using System.Web.Security;

namespace GiveCRM.Admin.Web.Services
{
    /// <summary>
    /// Describes the various results that can be returned when creating a user account.  Has a loose
    /// associating with <see cref="MembershipCreateStatus"/>
    /// </summary>
    public enum UserCreationResult
    {
        /// <summary>
        /// The user account was created successfully.
        /// </summary>
        Success,

        /// <summary>
        /// The email address provided is already registered.
        /// </summary>
        DuplicateEmail,

        /// <summary>
        /// The username provided is already registered.
        /// </summary>
        DuplicateUsername,

        /// <summary>
        /// The email address provided is not valid.  For example, it might
        /// be missing the @ symbol.
        /// </summary>
        InvalidEmail,

        /// <summary>
        /// The username provided is not valid.  For example, it might contain
        /// whitespace.
        /// </summary>
        InvalidUsername,

        /// <summary>
        /// The password provided is not valid.  For example, it might only
        /// contain lowercase letters.
        /// </summary>
        InvalidPassword
    }
}
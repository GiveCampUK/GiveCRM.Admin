namespace GiveCRM.Admin.Web.Services
{
    /// <summary>
    /// Simple interface to make controllers dealing with Membership more testable.
    /// </summary>
    public interface IMembershipService
    {
        /// <summary>
        /// Get a user account registered against the provided username.
        /// </summary>
        /// <param name="username">The username of the user to fetch.</param>
        /// <returns>If the username is registered to a user, then an <see cref="IMember" /> representing 
        /// the user is returned; otherwise, <c>null</c> is returned</returns>
        IMember GetUser(string username);

        /// <summary>
        /// Registers a new user account with the provided username, password and email address.
        /// </summary>
        /// <param name="username">The username to register on the new user account.</param>
        /// <param name="password">The password to register on the new user account.</param>
        /// <param name="email">The email address to register on the new user account.</param>
        /// <returns>A <see cref="UserCreationResult"/> value indicating whether or not the 
        /// user account was created successfully.  If it was not created successfully, the 
        /// specific reason for the failure is returned.</returns>
        UserCreationResult CreateUser(string username, string password, string email);
    }
}

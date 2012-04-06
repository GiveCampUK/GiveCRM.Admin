using System;
using System.Web.Security;

namespace GiveCRM.Admin.Web.Services
{
    public class AspMembershipMember : IMember
    {
        private readonly MembershipUser aspMembershipUser;

        public AspMembershipMember(MembershipUser aspMembershipUser)
        {
            if (aspMembershipUser == null)
            {
                throw new ArgumentNullException("aspMembershipUser");
            }

            this.aspMembershipUser = aspMembershipUser;
        }

        public string UserName
        {
            get { return aspMembershipUser.UserName; }
        }

        public string Email
        {
            get { return aspMembershipUser.Email; }
        }
    }
}
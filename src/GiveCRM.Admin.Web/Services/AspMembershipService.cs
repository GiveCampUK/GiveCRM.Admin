using System;
using System.Web.Security;

namespace GiveCRM.Admin.Web.Services
{
    public class AspMembershipService : IMembershipService
    {
        private readonly MembershipProvider membershipProvider;

        public AspMembershipService(MembershipProvider membershipProvider)
        {
            if (membershipProvider == null)
            {
                throw new ArgumentNullException("membershipProvider");
            }

            this.membershipProvider = membershipProvider;
        }

        public IMember GetUser(string username)
        {
            return new AspMembershipMember(membershipProvider.GetUser(username, false));
        }

        public MembershipCreateStatus CreateUser(string username, string password, string email)
        {
            System.Web.Security.MembershipCreateStatus providerCreateStatus;
            membershipProvider.CreateUser(username, password, email, string.Empty, string.Empty, true, new object(),
                                          out providerCreateStatus);

            return MapProviderCreateStatus(providerCreateStatus);
        }

        private MembershipCreateStatus MapProviderCreateStatus(System.Web.Security.MembershipCreateStatus providerCreateStatus)
        {
            switch (providerCreateStatus)
            {
                case System.Web.Security.MembershipCreateStatus.Success:
                    return MembershipCreateStatus.Success;
                case System.Web.Security.MembershipCreateStatus.DuplicateUserName:
                    return MembershipCreateStatus.DuplicateUsername;
                case System.Web.Security.MembershipCreateStatus.DuplicateEmail:
                    return MembershipCreateStatus.DuplicateEmail;
                case System.Web.Security.MembershipCreateStatus.InvalidUserName:
                    return MembershipCreateStatus.InvalidUsername;
                case System.Web.Security.MembershipCreateStatus.InvalidPassword:
                    return MembershipCreateStatus.InvalidPassword;
                case System.Web.Security.MembershipCreateStatus.InvalidEmail:
                    return MembershipCreateStatus.InvalidEmail;
                case System.Web.Security.MembershipCreateStatus.InvalidQuestion:
                case System.Web.Security.MembershipCreateStatus.InvalidAnswer:
                    return MembershipCreateStatus.Success;
                default:
                    throw new ArgumentException(
                        string.Format("Unsupported value of System.Web.Security.MembershipCreateStatus {0}",
                                      providerCreateStatus));
            }
        }
    }
}
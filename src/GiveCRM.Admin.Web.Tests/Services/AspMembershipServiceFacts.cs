using System;
using System.Web.Security;
using GiveCRM.Admin.Web.Services;
using NSubstitute;
using NUnit.Framework;

namespace GiveCRM.Admin.Web.Tests.Services
{
    [TestFixture]
    public class AspMembershipServiceFacts
    {
        [TestFixture]
        public class ConstructorShould
        {
            [Test]
            public void ThrowAnArgumentNullException_WhenMembershipProviderArgumentIsNull()
            {
                Assert.That(() => new AspMembershipService(null), Throws.InstanceOf<ArgumentNullException>());
            }
        }

        [TestFixture]
        public class GetUserShould
        {
            [Test]
            public void ReturnNull_WhenTheUserDoesNotExist()
            {
                var membershipProvider = Substitute.For<MembershipProvider>();
                membershipProvider.GetUser("non-existent", false).Returns((MembershipUser)null);
                var membership = new AspMembershipService(membershipProvider);

                var user = membership.GetUser("non-existent");

                Assert.That(user, Is.Null);
            }

            [Test]
            public void ReturnNull_WhenTheUsernameIsNull()
            {
                var membershipProvider = Substitute.For<MembershipProvider>();
                membershipProvider.GetUser(null, false).Returns((MembershipUser)null);
                var membership = new AspMembershipService(membershipProvider);
                
                var user = membership.GetUser(null);
                
                Assert.That(user, Is.Null);
            }

            [Test]
            public void ReturnNull_WhenTheUsernameIsEmpty()
            {
                var membershipProvider = Substitute.For<MembershipProvider>();
                membershipProvider.GetUser(string.Empty, false).Returns((MembershipUser)null);
                var membership = new AspMembershipService(membershipProvider);

                var user = membership.GetUser(string.Empty);
                
                Assert.That(user, Is.Null);
            }

            [Test]
            public void ReturnTheSpecifiedUser_WhenTheUsernameIsValidForARegisteredUser()
            {
                const string username = "valid.user";

                var membershipProvider = Substitute.For<MembershipProvider>();
                var validUser = Substitute.For<MembershipUser>();
                validUser.UserName.Returns(username);
                membershipProvider.GetUser(username, false).Returns(validUser);
                var membership = new AspMembershipService(membershipProvider);

                var user = membership.GetUser(username);

                Assert.That(user.UserName, Is.EqualTo(validUser.UserName));
            }
        }

        [TestFixture]
        public class CreateUserShould
        {
            [Test]
            public void ReturnSuccess_WhenTheUserIsCreatedSuccessfully()
            {
                
            }
        }
    }
}

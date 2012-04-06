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
                // Arrange
                const string username = "valid.user";

                var validUser = Substitute.For<MembershipUser>();
                validUser.UserName.Returns(username);
                
                var membershipProvider = Substitute.For<MembershipProvider>();
                membershipProvider.GetUser(username, false).Returns(validUser);
                
                var membership = new AspMembershipService(membershipProvider);

                // Act
                var user = membership.GetUser(username);

                // Assert
                Assert.That(user.UserName, Is.EqualTo(validUser.UserName));
            }

            [Test]
            public void CallAspMembershipProviderGetUser()
            {
                // Arrange
                const string username = "valid.user";

                var validUser = Substitute.For<MembershipUser>();
                validUser.UserName.Returns(username);
                
                var membershipProvider = Substitute.For<MembershipProvider>();
                membershipProvider.GetUser(username, false).Returns(validUser);

                var membership = new AspMembershipService(membershipProvider);

                // Act
                membership.GetUser(username);

                // Assert
                membershipProvider.Received().GetUser(username, false);
            }
        }

        [TestFixture]
        public class CreateUserShould
        {
            [Test]
            public void ReturnSuccess_WhenTheUserIsCreatedSuccessfully()
            {
                const string username = "foo";
                const string password = "bar123";
                const string email = "foo@gmail.com";

                var membershipProvider = Substitute.For<MembershipProvider>();
                var membershipUser = Substitute.For<MembershipUser>();
                MembershipCreateStatus membershipCreateStatus;
                membershipProvider.CreateUser(username, password, email, string.Empty, string.Empty, true, 
                                              Guid.NewGuid(), out membershipCreateStatus)
                                  .Returns(@params =>
                                  {
                                      @params[7] = MembershipCreateStatus.Success;
                                      return membershipUser;
                                  });

                var membership = new AspMembershipService(membershipProvider);
                var result = membership.CreateUser(username, password, email);

                Assert.That(result, Is.EqualTo(UserCreationResult.Success));
            }

            [Test]
            public void CallAspMembershipProviderCreateUser() 
            {
                const string username = "foo";
                const string password = "bar123";
                const string email = "foo@gmail.com";
                
                var membershipProvider = Substitute.For<MembershipProvider>();
                var membershipUser = Substitute.For<MembershipUser>();
                MembershipCreateStatus membershipCreateStatus;
                membershipProvider.CreateUser(username, password, email, string.Empty, string.Empty, true,
                                              Arg.Any<Guid>(), out membershipCreateStatus)
                                  .Returns(@params =>
                                  {
                                      @params[7] = MembershipCreateStatus.Success;
                                      return membershipUser;
                                  });

                var membership = new AspMembershipService(membershipProvider);
                membership.CreateUser(username, password, email);

                membershipProvider.Received().CreateUser(username, password, email, string.Empty, string.Empty, 
                                                         true, Arg.Any<Guid>(), out membershipCreateStatus);
            }
        }
    }
}

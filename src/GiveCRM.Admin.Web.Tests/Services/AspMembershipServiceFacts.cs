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
            const string Username = "foo";
            const string Password = "bar123";
            const string Email = "foo@gmail.com";

            [Test]
            public void ReturnSuccess_WhenTheUserIsCreatedSuccessfully()
            {
                var membershipProvider = CreateMockMembershipProvider(MembershipCreateStatus.Success);
                var membershipService = new AspMembershipService(membershipProvider);
                
                var result = membershipService.CreateUser(Username, Password, Email);

                Assert.That(result, Is.EqualTo(UserCreationResult.Success));
            }

            [Test]
            public void ReturnsDuplicateEmail_WhenTheEmailAddressIsAlreadyRegistered()
            {
                var membershipProvider = CreateMockMembershipProvider(MembershipCreateStatus.DuplicateEmail);
                var membershipService = new AspMembershipService(membershipProvider);
                
                var result = membershipService.CreateUser(Username, Password, Email);

                Assert.That(result, Is.EqualTo(UserCreationResult.DuplicateEmail));
            }

            [Test]
            public void CallAspMembershipProviderCreateUser()
            {
                var membershipProvider = CreateMockMembershipProvider(MembershipCreateStatus.Success);
                MembershipCreateStatus membershipCreateStatus;
                var membershipService = new AspMembershipService(membershipProvider);
                membershipService.CreateUser(Username, Password, Email);

                membershipProvider.Received().CreateUser(Username, Password, Email, string.Empty, string.Empty, 
                                                         true, Arg.Any<Guid>(), out membershipCreateStatus);
            }

            private static MembershipProvider CreateMockMembershipProvider(MembershipCreateStatus membershipCreateStatus)
            {
                var membershipProvider = Substitute.For<MembershipProvider>();
                var membershipUser = Substitute.For<MembershipUser>();
                MembershipCreateStatus returnedMembershipCreateStatus;
                membershipProvider.CreateUser(Username, Password, Email, string.Empty, string.Empty, true,
                                              Guid.NewGuid(), out returnedMembershipCreateStatus)
                    .Returns(@params =>
                                 {
                                     @params[7] = membershipCreateStatus;
                                     return membershipUser;
                                 });
                return membershipProvider;
            }
        }
    }
}

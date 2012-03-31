using System;
using System.Transactions;
using System.Web.Security;
using GiveCRM.Admin.BusinessLogic;
using GiveCRM.Admin.Models;

namespace GiveCRM.Admin.DataAccess
{
    public class CharityMembershipService : ICharityMembershipService
    {
        private readonly ICharityRepository charityRepository;
        private readonly ICharitiesMembershipRepository charitiesMembershipRepository;

        public CharityMembershipService(ICharityRepository charityRepository, ICharitiesMembershipRepository charitiesMembershipRepository)
        {
            if (charityRepository == null) throw new ArgumentNullException("charityRepository");
            if (charitiesMembershipRepository == null)
            {
                throw new ArgumentNullException("charitiesMembershipRepository");
            }

            this.charityRepository = charityRepository;
            this.charitiesMembershipRepository = charitiesMembershipRepository;
        }

        public bool RegisterUserAndCharity(RegistrationInfo registrationInfo)
        {
            bool result = false;
            var charity = new Charity
            {
                Name = registrationInfo.CharityName,
                RegisteredCharityNumber = registrationInfo.CharityName,
                SubDomain = registrationInfo.SubDomain
            };
            using (var scope = new TransactionScope())
            {
                var newCharity = charityRepository.Save(charity);
                if (newCharity == null) throw new ArgumentNullException("newCharity");


                // Attempt to register the user
                MembershipCreateStatus createStatus;
                var user = Membership.CreateUser(registrationInfo.UserIdentifier, registrationInfo.Password, registrationInfo.UserIdentifier, null, null, true, null, out createStatus);
                
                if (user == null) throw new ArgumentNullException("user");

                var charityMembership = new CharityMembership
                {
                    CharityId = newCharity.Id,
                    UserName = user.UserName
                };
                var newCharityMembership = charitiesMembershipRepository.Save(charityMembership);

                if (createStatus == MembershipCreateStatus.Success && newCharityMembership != null)
                {
                    result = true;
                    scope.Complete();
                }
                
            }
            return result;
        }
    }
}

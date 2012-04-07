using System;
using System.Transactions;
using GiveCRM.Admin.Models;

namespace GiveCRM.Admin.BusinessLogic
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

        public bool RegisterCharityWithUser(RegistrationInfo registrationInfo, User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

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

                var charityMembership = new CharityMembership
                {
                    CharityId = newCharity.Id,
                    UserName = registrationInfo.UserIdentifier
                };
                var newCharityMembership = charitiesMembershipRepository.Save(charityMembership);

                if (newCharityMembership != null)
                {
                    result = true;
                    scope.Complete();
                }
            }
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiveCRM.Admin.BusinessLogic;
using GiveCRM.Admin.Models;
using Simple.Data;

namespace GiveCRM.Admin.DataAccess
{
    public class CharitiesMemberships : ICharitiesMembershipRepository
    {
        private readonly dynamic _db = Database.OpenNamedConnection("GiveCRMAdmin");

        public CharityMembership GetById(int id)
        {
            return _db.CharityMembership.FindById(id);
        }

        public IEnumerable<CharityMembership> GetAll()
        {
            return _db.CharityMembership.All().Cast<CharityMembership>();
        }

        public CharityMembership GetByUserId(string userId)
        {
            return _db.CharityMembership.FindByUserId(userId);
        }

        public CharityMembership Save(CharityMembership charityMembership)
        {
            return _db.CharityMembership.Upsert(charityMembership);
        }

        public bool Delete(CharityMembership charityMembership)
        {
            return DeleteById(charityMembership.Id);
        }

        public bool DeleteById(int id)
        {
            return _db.CharityMembership.DeleteById(id);
        }
    }
}

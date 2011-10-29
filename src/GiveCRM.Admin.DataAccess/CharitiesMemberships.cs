using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiveCRM.Admin.Models;
using Simple.Data;

namespace GiveCRM.Admin.DataAccess
{
    public class CharitiesMemberships
    {
        private readonly dynamic _db = Database.OpenNamedConnection("GiveCRMAdmin");

        public CharityMembership Get(int id)
        {
            return _db.CharityMembership.FindById(id);
        }

        public IEnumerable<CharityMembership> All()
        {
            return _db.CharityMembership.All().Cast<CharityMembership>();
        }

        public CharityMembership GetByUserId(string userId)
        {
            return _db.CharityMembership.FindByUserId(userId);
        }

        public CharityMembership Insert(CharityMembership charityMembership)
        {
            return _db.CharityMembership.Insert(charityMembership);
        }

        public void Update(CharityMembership charityMembership)
        {
            _db.CharityMembership.UpdateById(charityMembership);
        }

        public void Delete(CharityMembership charityMembership)
        {
            _db.CharityMembership.DeleteById(charityMembership.Id);
        }
    }
}

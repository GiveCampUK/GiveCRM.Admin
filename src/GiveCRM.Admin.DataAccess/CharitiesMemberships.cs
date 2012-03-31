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
        private readonly dynamic db;

        public CharitiesMemberships(dynamic connection)
        {
            db = connection;
        }

        public CharitiesMemberships() : this((Database)Database.OpenNamedConnection("GiveCRMAdmin"))
        {
            // TODO: Delete this constructor.
        }

        public CharityMembership GetById(int id)
        {
            return db.CharityMembership.FindById(id);
        }

        public IEnumerable<CharityMembership> GetAll()
        {
            return db.CharityMembership.All().Cast<CharityMembership>();
        }

        public CharityMembership GetByUserId(string userId)
        {
            return db.CharityMembership.FindByUserId(userId);
        }

        public CharityMembership Save(CharityMembership charityMembership)
        {
            return db.CharityMembership.Upsert(charityMembership);
        }

        public bool Delete(CharityMembership charityMembership)
        {
            return DeleteById(charityMembership.Id);
        }

        public bool DeleteById(int id)
        {
            return db.CharityMembership.DeleteById(id) == 1;
        }
    }
}

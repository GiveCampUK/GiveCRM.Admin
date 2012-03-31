using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiveCRM.Admin.BusinessLogic;
using GiveCRM.Admin.Models;

namespace GiveCRM.Admin.DataAccess
{
    public class Charities : ICharityRepository
    {
        private readonly dynamic db;

        public Charities(dynamic db)
        {
            this.db = db;
        }

        public Charity GetById(int id)
        {
            return db.Charity.FindById(id);
        }

        public IEnumerable<Charity> GetAll()
        {
            return db.Charity.All().Cast<Charity>();
        }

        public Charity GetByUserName(string userName)
        {
            return db.Charity.FindByUserId(userName);
        }

        public Charity Save(Charity charity)
        {
            return db.Charity.Upsert(charity);
        }

        public bool Delete(Charity charity)
        {
            return DeleteById(charity.Id);
        }

        public bool DeleteById(int id)
        {
            return db.Charity.DeleteById(id) == 1;
        }
    }
}

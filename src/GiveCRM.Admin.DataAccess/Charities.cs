using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiveCRM.Admin.Models;
using Simple.Data;

namespace GiveCRM.Admin.DataAccess
{
    public class Charities
    {
        private readonly dynamic _db = Database.OpenNamedConnection("GiveCRMAdmin");

        public Charity Get(int id)
        {
            return _db.Charity.FindById(id);
        }

        public IEnumerable<Charity> All()
        {
            return _db.Charity.All().Cast<Charity>();
        }

        public Charity GetByUserId(string userId)
        {
            return _db.Charity.FindByUserId(userId);
        }

        public Charity Insert(Charity charity)
        {
            return _db.Charity.Insert(charity);
        }

        public void Update(Charity charity)
        {
            _db.Charity.UpdateById(charity);
        }

        public void Delete(Charity charity)
        {
            _db.Charity.DeleteById(charity.Id);
        }
    }
}

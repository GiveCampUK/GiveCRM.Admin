using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiveCRM.Admin.Models;
using NUnit.Framework;
using Simple.Data;

namespace GiveCRM.Admin.DataAccess.Test
{
    [TestFixture]
    public class CharitiesMembershipRepositoryFacts
    {
        [TestFixture]
        public class GetAllShould
        {
            private readonly dynamic db = Database.OpenFile("TestDB.sdf");
            [SetUp]
            public void SetUp()
            {
                db.CharityMembership.DeleteAll();
            }

            [Test]
            public void ReturnAnEmptyCollection_WhenThereAreNoCharityMembershipsDefined()
            {
                var allCharitiesMemberships = new CharitiesMemberships(db).GetAll();
                Assert.That(allCharitiesMemberships, Is.Empty);
            }

            [Test]
            public void ReturnACollectionOfItems_WhenThereAreCharityMembershipsDefined()
            {
                var charityMembership = new CharityMembership
                                            {
                                                Id = 1,
                                                CharityId = 1,
                                                UserName = "test"
                                            };
                var charitiesMemberships = new CharitiesMemberships(db);
                charitiesMemberships.Save(charityMembership);

                var allCharitiesMemberships = charitiesMemberships.GetAll();
                Assert.That(allCharitiesMemberships.Single().Id, Is.EqualTo(charityMembership.Id));
            }
        }

        [TestFixture]
        public class GetByIdShould
        {
            
        }

        [TestFixture]
        public class SaveShould
        {

        }

        [TestFixture]
        public class DeleteShould
        {

        }

        [TestFixture]
        public class DeleteByIdShould
        {

        }
    }
}

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
    public class CharitiesMembershipsFacts
    {
        private static readonly dynamic db = Database.OpenFile("TestDB.sdf");

        private static void TestSetUp()
        {
            db.CharityMembership.DeleteAll();
        }

        [TestFixture]
        public class GetAllShould
        {
            [SetUp]
            public void Setup()
            {
                TestSetUp();
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
                                                CharityId = 1,
                                                UserName = "test"
                                            };
                var charitiesMemberships = new CharitiesMemberships(db);
                charitiesMemberships.Save(charityMembership);

                var allCharitiesMemberships = charitiesMemberships.GetAll().ToList();
                Assert.That(allCharitiesMemberships.Single().CharityId, Is.EqualTo(charityMembership.CharityId));
                Assert.That(allCharitiesMemberships.Single().UserName, Is.EqualTo(charityMembership.UserName));
            }
        }

        [TestFixture]
        public class GetByIdShould
        {
            [SetUp]
            public void SetUp()
            {
                db.CharityMembership.DeleteAll();
            }

            [Test]
            public void ReturnNull_WhenNoCharityMembershipExistsWithTheGivenId()
            {
                var charitiesMemberships = new CharitiesMemberships(db);
                var nonExistent = charitiesMemberships.GetById(4096);

                Assert.That(nonExistent, Is.Null);
            }

            [Test]
            public void ReturnTheCharityMembershipWithTheSpecifiedId_WhenACharityMembershipExistsWithTheGivenId()
            {
                var charityMembership = new CharityMembership
                                            {
                                                CharityId = 35,
                                                UserName = "test"
                                            };
                var charitiesMemberships = new CharitiesMemberships(db);

                var createdCharityMembership = charitiesMemberships.Save(charityMembership);
                int createdId = createdCharityMembership.Id;

                var foundCharityMembership = charitiesMemberships.GetById(createdId);
                Assert.That(foundCharityMembership.Id, Is.EqualTo(createdId));
            }
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

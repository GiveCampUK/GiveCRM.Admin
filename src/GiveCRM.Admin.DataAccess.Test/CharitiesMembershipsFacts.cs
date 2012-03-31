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
        [TestFixture]
        public class GetAllShould
        {
            private readonly dynamic db = Database.OpenFile("TestDB.sdf");

            [SetUp]
            public void Setup()
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
            private readonly dynamic db = Database.OpenFile("TestDB.sdf");

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
            private readonly dynamic db = Database.OpenFile("TestDB.sdf");

            [SetUp]
            public void SetUp()
            {
                db.CharityMembership.DeleteAll();
            }

            [Test]
            public void ReturnTheCreatedCharityMembership_WhenTheSaveIsSuccessful()
            {
                var charityMembership = new CharityMembership
                                            {
                                                CharityId = 30,
                                                UserName = "test"
                                            };
                var charitiesMemberships = new CharitiesMemberships(db);
                
                var createdCharityMembership = charitiesMemberships.Save(charityMembership);
                
                Assert.That(createdCharityMembership.CharityId, Is.EqualTo(charityMembership.CharityId));
                Assert.That(createdCharityMembership.UserName, Is.EqualTo(charityMembership.UserName));
            }
        }

        [TestFixture]
        public class DeleteShould
        {
            private readonly dynamic db = Database.OpenFile("TestDB.sdf");
            private CharityMembership charityMembership;
            
            [SetUp]
            public void SetUp()
            {
                db.CharityMembership.DeleteAll();

                charityMembership = db.CharityMembership.Insert(new CharityMembership
                                                                    {
                                                                        CharityId = 58,
                                                                        UserName = "test"
                                                                    });
            }

            [Test]
            public void ReturnTrue_WhenTheDeletionIsSuccessful()
            {
                var charitiesMemberships = new CharitiesMemberships(db);
                Assert.That(charitiesMemberships.Delete(charityMembership), Is.True);
            }

            [Test]
            public void ReturnFalse_WhenTheDeletionFails()
            {
                var nonExistentCharityMembership = new CharityMembership
                                                       {
                                                           Id = 4096,
                                                           CharityId = 9612,
                                                           UserName = "test"
                                                       };
                var charitiesMemberships = new CharitiesMemberships(db);

                Assert.That(charitiesMemberships.Delete(nonExistentCharityMembership), Is.False);
            }
        }

        [TestFixture]
        public class DeleteByIdShould
        {
            private readonly dynamic db = Database.OpenFile("TestDB.sdf");
            private CharityMembership charityMembership;

            [SetUp]
            public void SetUp()
            {
                db.CharityMembership.DeleteAll();

                charityMembership = db.CharityMembership.Insert(new CharityMembership
                                                                    {
                                                                        CharityId = 58,
                                                                        UserName = "test"
                                                                    });
            }

            [Test]
            public void ReturnTrue_WhenTheDeletionIsSuccessful()
            {
                var charitiesMemberships = new CharitiesMemberships(db);
                Assert.That(charitiesMemberships.DeleteById(charityMembership.Id), Is.True);
            }

            [Test]
            public void ReturnFalse_WhenTheDeletionFails()
            {
                var charitiesMemberships = new CharitiesMemberships(db);
                Assert.That(charitiesMemberships.DeleteById(68), Is.False);
            }
        }
    }
}

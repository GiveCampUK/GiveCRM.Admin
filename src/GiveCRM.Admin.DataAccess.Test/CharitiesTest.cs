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
    public class CharitiesTest
    {
        const string TestName = "Royal Society for Protection of Tests";
        const string TestUserId = "bob";
        const string TestPassword = "qwertyuiop";
        const string TestUserName = "Bob The Builder";
        const string TestRegisteredCharityNumber = "1";
        const string TestSubDomain = "rspt";

        [SetUp]
        public void SetUp()
        {
            Database.OpenFile("TestDB.sdf").Charity.DeleteAll();
        }

        [Test]
        public void InitialCreate()
        {
            var actual = CreateCharity();

            Assert.AreNotEqual(0, actual.Id);
            Assert.AreEqual(TestName, actual.Name);
        }

        [Test]
        public void GetByUserId()
        {
            CreateCharity();

            var actual = new Charities().GetByUserName(TestUserId);
            Assert.IsNotNull(actual);
        }

        [Test]
        public void TestUpdateWithAdditionalInfo()
        {
            var target = CreateCharity();
            target.RegisteredCharityNumber = TestRegisteredCharityNumber;
            target.SubDomain = TestSubDomain;

            var data = new Charities();
            data.Save(target);

            var actual = data.GetById(target.Id);

            Assert.AreEqual(TestName, actual.Name);
            Assert.AreEqual(TestRegisteredCharityNumber, actual.RegisteredCharityNumber);
            Assert.AreEqual(TestSubDomain, actual.SubDomain);
        }

        [Test]
        public void TestGetAll()
        {
            var charity = CreateCharity();

            var actual = new Charities().GetAll().Single();

            Assert.That(actual.Name, Is.EqualTo(charity.Name));
            Assert.That(actual.UserId, Is.EqualTo(charity.UserId));
            Assert.That(actual.EncryptedPassword, Is.EqualTo(charity.EncryptedPassword));
            Assert.That(actual.RegisteredCharityNumber, Is.EqualTo(charity.RegisteredCharityNumber));
            Assert.That(actual.SubDomain, Is.EqualTo(charity.SubDomain));
            Assert.That(actual.Salt, Is.EqualTo(charity.Salt));
        }

        private static Charity CreateCharity()
        {
            var charity = new Charity
                              {
                                  Name = TestName,
                                  UserId = TestUserId,
                                  EncryptedPassword = Encoding.UTF8.GetBytes(TestPassword),
                                  RegisteredCharityNumber = TestRegisteredCharityNumber,
                                  SubDomain = TestSubDomain,
                                  Salt = Encoding.UTF8.GetBytes("passwordSalt")
                              };

            var target = new Charities();

            var actual = target.Save(charity);
            return actual;
        }
    }
}

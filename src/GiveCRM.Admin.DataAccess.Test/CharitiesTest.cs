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
            Database.OpenNamedConnection("GiveCRMAdmin").Charity.DeleteAll();
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

            var actual = new Charities().GetByUserId(TestUserId);
            Assert.IsNotNull(actual);
        }

        [Test]
        public void TestUpdateWithAdditionalInfo()
        {
            var target = CreateCharity();
            target.RegisteredCharityNumber = TestRegisteredCharityNumber;
            target.SubDomain = TestSubDomain;

            var data = new Charities();
            data.Update(target);

            var actual = data.Get(target.Id);

            Assert.AreEqual(TestName, actual.Name);
            Assert.AreEqual(TestRegisteredCharityNumber, actual.RegisteredCharityNumber);
            Assert.AreEqual(TestSubDomain, actual.SubDomain);
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

            var actual = target.Insert(charity);
            return actual;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Data;

namespace GiveCRM.Admin.DataAccess.Test
{
    [TestFixture]
    public class CharitiesMembershipRepositoryFacts
    {
        [SetUp]
        public void SetUp()
        {
            Database.OpenFile("TestDB.sdf").CharitiesMemberships.DeleteAll();
        }

        [TestFixture]
        public class GetAllShould
        {
            [Test]
            public void ReturnAnEmptyCollection_WhenThereAreNoCharityMembershipsDefined()
            {
                var allCharitiesMemberships = new CharitiesMemberships().GetAll();
                Assert.That(allCharitiesMemberships, Is.Empty);
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

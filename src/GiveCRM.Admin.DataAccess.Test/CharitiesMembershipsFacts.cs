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
            Database.OpenNamedConnection("GiveCRMAdmin").Charities.DeleteAll();
        }

        [TestFixture]
        public class GetAllShould
        {
            [Test]
            public void ReturnAnEmptyCollection_WhenThereAreNoCharityMembershipsDefined()
            {
                
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

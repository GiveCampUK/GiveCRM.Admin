using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace GiveCRM.Admin.Models.Test
{
    [TestFixture]
    public class CharityTest
    {
        [Test]
        public void TestPasswordEncryption()
        {
            const string password = "Bunch of flowers!";
            var target = new Charity();
            target.SetPassword(password);
            Assert.IsTrue(target.VerifyPassword(password));
        }
    }
}

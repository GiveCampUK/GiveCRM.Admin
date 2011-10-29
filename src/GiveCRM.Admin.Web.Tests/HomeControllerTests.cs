using GiveCRM.Admin.Web.Controllers;
using MvcContrib.TestHelper;
using NUnit.Framework;

namespace GiveCRM.Admin.Web.Tests
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class HomeControllerTests
    {
        [Test]
        public void Index_ReturnsView()
        {
            var controller = new HomeController();
            var result = controller.Index();

            result.AssertViewRendered();
        }
    }
    // ReSharper restore InconsistentNaming
}
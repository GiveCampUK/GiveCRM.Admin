using System.Web;
using GiveCRM.Admin.Web.Controllers;
using Moq;
using NUnit.Framework;
using System.Web.Mvc;

namespace GiveCRM.Admin.Web.Tests
{
    [TestFixture]
    public class ExcelImportController_Upload_Should
    {
        private ExcelImportController controller;

        [SetUp]
        public void SetUp()
        {
            controller = new ExcelImportController();
        }

        [Test]
        public void ReturnToIndexView_WhenNoFileIsSelectedForUpload()
        {
            var result = controller.Upload(null) as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }

        [Test]
        public void DisplayAnErrorMesage_WhenNoFileIsUploaded()
        {
            var result = controller.Upload(null) as ViewResult;
            Assert.IsNotNull(result.ViewBag.Error);
        }

        [Test]
        public void ReturnToIndexView_WhenAnEmptyFileIsUploaded()
        {
            var file = new Mock<HttpPostedFileBase>();
            file.Setup(f => f.ContentLength).Returns(0);

            var result = controller.Upload(file.Object) as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
        }

        [Test]
        public void DisplayAnErrorMessage_WhenAnEmptyFileIsUploaded()
        {
            var file = new Mock<HttpPostedFileBase>();
            file.Setup(f => f.ContentLength).Returns(0);

            var result = controller.Upload(file.Object) as ViewResult;

            Assert.IsNotNull(result.ViewBag.Error);
        }
    }
}

using System.Web;
using GiveCRM.Admin.Web.Controllers;
using GiveCRM.Admin.Web.Interfaces;
using GiveCRM.ImportExport;
using Moq;
using NUnit.Framework;
using System.Web.Mvc;

namespace GiveCRM.Admin.Web.Tests
{
    [TestFixture]
    public class ExcelImportController_Upload_Should
    {
        private ExcelImportController controller;
        
        private const string DashboardControllerName = "Dashboard";
        private const string IndexActionName = "Index";

        private const int ZeroFileLength = 0;
        private const int NonZeroFileLength = 4096;

        [SetUp]
        public void SetUp()
        {
            var fileSystem = new Mock<IFileSystemWrapper>();
            var excelImporter = new Mock<IExcelImport>();
            controller = new ExcelImportController(excelImporter.Object, fileSystem.Object);
        }

        [Test]
        public void ReturnToIndexView_WhenNoFileIsSelectedForUpload()
        {
            var result = controller.Upload(null) as ViewResult;
            Assert.AreEqual(IndexActionName, result.ViewName);
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
            file.Setup(f => f.ContentLength).Returns(ZeroFileLength);

            var result = controller.Upload(file.Object) as ViewResult;

            Assert.AreEqual(IndexActionName, result.ViewName);
        }

        [Test]
        public void DisplayAnErrorMessage_WhenAnEmptyFileIsUploaded()
        {
            var file = new Mock<HttpPostedFileBase>();
            file.Setup(f => f.ContentLength).Returns(ZeroFileLength);

            var result = controller.Upload(file.Object) as ViewResult;

            Assert.IsNotNull(result.ViewBag.Error);
        }

        [Test]
        public void ReturnToIndexView_WhenANonExcelFileIsUploaded()
        {
            var file = new Mock<HttpPostedFileBase>();
            file.Setup(f => f.ContentLength).Returns(NonZeroFileLength);
            file.Setup(f => f.FileName).Returns("A Text File.txt");

            var result = controller.Upload(file.Object) as ViewResult;

            Assert.AreEqual(IndexActionName, result.ViewName);
        }

        [Test]
        public void DisplayAnErrorMessage_WhenANonExcelFileIsUploaded()
        {
            var file = new Mock<HttpPostedFileBase>();
            file.Setup(f => f.ContentLength).Returns(NonZeroFileLength);
            file.Setup(f => f.FileName).Returns("A Text File.txt");

            var result = controller.Upload(file.Object) as ViewResult;

            Assert.IsNotNull(result.ViewBag.Error);
        }

        [Test]
        public void NotReturnAViewResult_WhenAnExcelFileIsUploaded_OldFormat()
        {
            var file = new Mock<HttpPostedFileBase>();
            file.Setup(f => f.ContentLength).Returns(NonZeroFileLength);
            file.Setup(f => f.FileName).Returns("An old-format Excel file.xls");

            var result = controller.Upload(file.Object);

            Assert.IsNotInstanceOf<ViewResult>(result);
        }

        [Test]
        public void NotReturnAViewResult_WhenAnExcelFileIsUploaded_NewFormat()
        {
            var file = new Mock<HttpPostedFileBase>();
            file.Setup(f => f.ContentLength).Returns(NonZeroFileLength);
            file.Setup(f => f.FileName).Returns("A new-format Excel file.xlsx");

            var result = controller.Upload(file.Object);

            Assert.IsNotInstanceOf<ViewResult>(result);
        }

        [Test]
        public void RedirectToDashboardIndex_WhenFileIsValidForImport()
        {
            var file = new Mock<HttpPostedFileBase>();
            file.Setup(f => f.ContentLength).Returns(NonZeroFileLength);
            file.Setup(f => f.FileName).Returns("Valid file.xls");

            var result = controller.Upload(file.Object) as RedirectToRouteResult;

            Assert.AreEqual(DashboardControllerName, result.RouteValues["controller"]);
            Assert.AreEqual(IndexActionName, result.RouteValues["action"]);
        }
    }
}

using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using GiveCRM.Admin.Web.Interfaces;
using GiveCRM.Admin.Web.ViewModels;
using GiveCRM.ImportExport;

namespace GiveCRM.Admin.Web.Controllers
{
    public class ExcelImportController : AsyncController
    {
        private const string ExcelFileExtension_OldFormat = ".xls";
        private const string ExcelFileExtension_NewFormat = ".xlsx";

        private IFileSystemWrapper fileSystemWrapper;
        private IExcelImport excelImporter;

        public ExcelImportController(IExcelImport excelImporter, IFileSystemWrapper fileSystemWrapper)
        {
            if (excelImporter == null)
            {
                throw new ArgumentNullException("excelImporter");
            }

            if (fileSystemWrapper == null)
            {
                throw new ArgumentNullException("fileSystemWrapper");
            }

            this.excelImporter = excelImporter;
            this.fileSystemWrapper = fileSystemWrapper;
        }

        public ActionResult Index()
        {
           return View(new ExcelImportViewModel());
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return ErrorView("You did not select a file for upload.");
            }

            if (file.ContentLength <= 0)
            {
                return ErrorView("The file you uploaded was empty.");
            }

            if (!IsValidFileExtension(file.FileName))
            {
                return ErrorView("The file you uploaded does not appear to be an Excel file.");
            }

            // Process the file
            BeginImportAsync(file.FileName);

            return RedirectToAction("Index", "Dashboard");
        }

        private ActionResult ErrorView(string message)
        {
            ViewBag.Error = message;
            return View("Index", new ExcelImportViewModel());
        }

        private bool IsValidFileExtension(string fileName)
        {
            return fileName.EndsWith(ExcelFileExtension_OldFormat) || fileName.EndsWith(ExcelFileExtension_NewFormat);
        }

        private void BeginImportAsync(string fileName)
        {
            AsyncManager.OutstandingOperations.Increment();

            Stream fileStream = fileSystemWrapper.Open(fileName, FileMode.Open, FileAccess.Read);
            
            excelImporter.Open(fileStream, ExcelFileType.XLS, true);
            excelImporter.GetRowsAsKeyValuePairs(0); //Simple.Data
        }
    }
}

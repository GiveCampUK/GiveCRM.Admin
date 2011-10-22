using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GiveCRM.Admin.Web.Controllers
{
    public class ExcelImportController : Controller
    {
        private const string ExcelMimeType = "application/vnd.ms-excel";
        private const string ExcelTemplatePath = "GiveCRM_Template.xls";

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (file == null)
            {
                ViewBag.Error = "You did not select a file for upload.";
                return View();
            }
            
            if (file.ContentLength <= 0)
            {
                ViewBag.Error = "The file you uploaded was empty.";
                return View();
            }

            return RedirectToAction("Index", "Dashboard");
        }

        public ActionResult Template()
        {
            return File(ExcelTemplatePath, ExcelMimeType);
        }
    }
}

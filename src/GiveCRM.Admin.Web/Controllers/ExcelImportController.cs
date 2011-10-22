using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GiveCRM.Admin.Web.ViewModels;

namespace GiveCRM.Admin.Web.Controllers
{
    public class ExcelImportController : Controller
    {
        public ActionResult Index()
        {
           return View(new ExcelImportViewModel());
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                ViewBag.Error = "You did not select a file for upload.";
                return View("Index", new ExcelImportViewModel());
            }
            
            if (file.ContentLength <= 0)
            {
                ViewBag.Error = "The file you uploaded was empty.";
                return View("Index");
            }

            // Process the file

            return RedirectToAction("Index", "Dashboard");
        }
    }
}

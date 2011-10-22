using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using GiveCRM.Admin.Web.Interfaces;
using GiveCRM.Admin.Web.ViewModels.SignUp;

namespace GiveCRM.Admin.Web.Controllers
{
    public class SignUpController : Controller
    {
        private readonly IConfiguration configuration;

        // TODO IoC here
        public SignUpController() : this(new HardCodedConfiguration())
        { }

        public SignUpController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult SignUp(RequiredInfo requiredInfo)
        {
            /*
            Add membership record
            Queue activation email sending
            Queue provisioning 
            */
            TempData["CharityName"] = requiredInfo.CharityName;

            return RedirectToAction("Complete");
        }

        public ActionResult Complete()
        {
            var charityName = TempData["CharityName"] as string;

            var additionalInfo = new AdditionalInfo
                                {
                                    SubDomain = GetSubDomainFromCharityName(charityName)
                                };
            var viewModel = new Complete
                                {
                                    AdditionalInfo = additionalInfo,
                                    Configuration = configuration
                                };

            return View(viewModel);
        }

        private static string GetSubDomainFromCharityName(string charityName)
        {
            var result = Regex.Replace(charityName, @"[\s]", "-");
            return Regex.Replace(result, @"[^\w-]", "");
        }
    }
}

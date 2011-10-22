using System.Text.RegularExpressions;
using System.Web.Mvc;
using GiveCRM.Admin.Web.Interfaces;
using GiveCRM.Admin.Web.Services;
using GiveCRM.Admin.Web.ViewModels.SignUp;

namespace GiveCRM.Admin.Web.Controllers
{
    public class SignUpController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly ISignUpQueueingService signUpQueueingService;

        // TODO IoC here
        public SignUpController() : this(new HardCodedConfiguration(), new SignUpNonQueueingService())
        { }

        public SignUpController(IConfiguration configuration, ISignUpQueueingService signUpQueueingService)
        {
            this.configuration = configuration;
            this.signUpQueueingService = signUpQueueingService;
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult SignUp(RequiredInfo requiredInfo)
        {
            if (!ModelState.IsValid)
            {
                return View(requiredInfo);
            }

            var subDomain = GetSubDomainFromCharityName(requiredInfo.CharityName);
            /*
            Add membership record inc. domain information
            */
            signUpQueueingService.QueueEmail();
            signUpQueueingService.QueueProvisioning();

            TempData["SubDomain"] = subDomain;

            return RedirectToAction("Complete");
        }

        [HttpGet]
        public ActionResult Complete()
        {
            var subDomain = TempData["SubDomain"] as string;

            var viewModel = new Complete
                                {
                                    SubDomain = subDomain
                                }.WithConfig(configuration);

            return View(viewModel);
        }

        private static string GetSubDomainFromCharityName(string charityName)
        {
            var result = Regex.Replace(charityName, @"[\s]", "-");
            return Regex.Replace(result, @"[^\w-]", "");
        }

        [HttpPost]
        public ActionResult StoreAdditionalInfo(Complete complete)
        {
            var viewModel = complete.WithConfig(configuration);

            if (!ModelState.IsValid)
            {
                return View("Complete", viewModel);
            }

            return View("Complete", viewModel);
        }
    }
}

using System.Configuration;
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

        public ActionResult SignUp()
        {
            /*
            Add membership record
            Queue activation email sending
            Queue provisioning 
            */ 

            return RedirectToAction("Complete");
        }

        public ActionResult Complete()
        {
            var viewModel = new AdditionalInfo
                                {
                                    BaseDomain = configuration.BaseDomain,
                                    SubDomain = "mycharity"
                                };

            return View(viewModel);
        }
    }
}

using System.Web.Mvc;

namespace GiveCRM.Admin.Web.Controllers
{
    public class SignUpController : Controller
    {
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
            return View();
        }
    }
}

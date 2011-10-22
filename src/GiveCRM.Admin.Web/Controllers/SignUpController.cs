using System.Web.Mvc;

namespace GiveCRM.Admin.Web.Controllers
{
    public class SignUpController : Controller
    {
        public ActionResult SignUp()
        {
            return RedirectToAction("Complete");
        }

        public ActionResult Complete()
        {
            return View();
        }
    }
}

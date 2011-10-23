using System.Web.Mvc;
using GiveCRM.Models;
using Simple.Data;

namespace GiveCRM.Admin.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly dynamic db = Database.OpenNamedConnection("GiveCRM");

        //
        // GET: /Dashboard/

        public ActionResult Index()
        {
            var allMembers = db.Members.All().Cast<Member>();

            return View(allMembers);
        }

    }
}

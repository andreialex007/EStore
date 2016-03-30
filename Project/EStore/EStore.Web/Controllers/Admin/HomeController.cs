using System.Web.Mvc;

namespace EStore.Web.Controllers.Admin
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Home/Index");
        }
    }
}
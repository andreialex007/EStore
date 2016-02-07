using System.Web.Mvc;

namespace EStore.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Content("working...");
        }
    }
}
using System.Web.Mvc;
using ControllerBase = EStore.Web.Controllers._Common.ControllerBase;

namespace EStore.Web.Controllers.Admin
{
    public class DashboardController : ControllerBase
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Views/Admin/Dashboard/Index.cshtml");
        }
    }
}
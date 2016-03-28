using System.Web.Mvc;
using ControllerBase = EStore.Web.Controllers._Common.ControllerBase;

namespace EStore.Web.Controllers
{
    public class ProductCategoriesController : ControllerBase
    {
        public ActionResult Index()
        {
            return View("ArticleCategories/Index");
        }
    }
}
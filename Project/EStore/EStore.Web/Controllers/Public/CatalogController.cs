using System.Web.Mvc;
using ControllerBase = EStore.Web.Controllers._Common.ControllerBase;

namespace EStore.Web.Controllers.Public
{
    [AllowAnonymous]
    public class CatalogController : ControllerBase
    {
        [ChildActionOnly]
        public ActionResult Menu()
        {
            var items = Service.ProductCategory.AllCategoriesHierarchy();
            return PartialView("~/Views/Public/Catalog/Menu.cshtml", items);
        }

        [HttpGet]
        public ActionResult Index()
        {
            var items = Service.ProductCategory.AllCategoriesHierarchy();
            return View("~/Views/Public/Catalog/Index.cshtml", items);
        }

        [HttpGet]
        public ActionResult Catalog(long id)
        {
            var model = Service.Product.ByCategoryId(id);
            return View("~/Views/Public/Catalog/List.cshtml", model);
        }
    }
}
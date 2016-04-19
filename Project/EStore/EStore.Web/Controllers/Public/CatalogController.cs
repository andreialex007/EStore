using System.Web.Mvc;
using ControllerBase = EStore.Web.Controllers._Common.ControllerBase;

namespace EStore.Web.Controllers.Public
{
    [AllowAnonymous]
    public class CatalogController : ControllerBase
    {
        public ActionResult Menu()
        {
            var items = Service.ProductCategory.AllCategoriesHierarchy();
            return PartialView("~/Views/Public/Catalog/Menu.cshtml", items);
        }
    }
}
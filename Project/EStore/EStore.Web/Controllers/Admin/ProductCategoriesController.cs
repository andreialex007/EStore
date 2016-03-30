using System.Web.Mvc;
using EStore.BL.Exceptions;
using EStore.BL.Models.Product;
using EStore.DL.Mapping;
using ControllerBase = EStore.Web.Controllers._Common.ControllerBase;

namespace EStore.Web.Controllers.Admin
{
    public class ProductCategoriesController : ControllerBase
    {
        public ActionResult Index()
        {
            var items = Service.ProductCategory.All();
            return View("~/Views/Admin/ProductCategories/Index.cshtml", items);
        }

        [HttpGet]
        public ActionResult Edit(long id = 0)
        {
            var categoryItem = Service.ProductCategory.Edit(id);
            return View("~/Views/Admin/ProductCategories/Edit.cshtml", categoryItem);
        }

        [HttpPost]
        public ActionResult Edit(ProductCategoryItem item)
        {
            try
            {
                Service.ProductCategory.Save(item);
                return RedirectToRouteNotify("EditProductCategory", new { id = item.Id });
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex);
                Service.ProductCategory.AppendData(item);
                return View("~/Views/Admin/ProductCategories/Edit.cshtml", item);
            }
        }

        [HttpPost]
        public JsonResult Delete(long id)
        {
            Service.Delete<tblProductCategory>(id);
            return SuccessJsonResult();
        }
    }
}
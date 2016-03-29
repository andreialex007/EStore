using System.Web.Mvc;
using EStore.BL.Exceptions;
using EStore.BL.Models;
using EStore.DL.Mapping;
using ControllerBase = EStore.Web.Controllers._Common.ControllerBase;

namespace EStore.Web.Controllers
{
    public class ProductCategoriesController : ControllerBase
    {
        public ActionResult Index()
        {
            var items = Service.ProductCategory.All();
            return View("ProductCategories/Index", items);
        }

        [HttpGet]
        public ActionResult Edit(long id = 0)
        {
            var categoryItem = Service.ProductCategory.Edit(id);
            return View("ProductCategories/Edit", categoryItem);
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
                return View("ProductCategories/Edit", item);
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
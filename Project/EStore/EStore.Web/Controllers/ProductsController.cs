using System.Web.Mvc;
using EStore.BL.Exceptions;
using EStore.BL.Models.Product;
using EStore.BL.Models._Common;
using EStore.DL.Mapping;
using EStore.Web.Code;
using ControllerBase = EStore.Web.Controllers._Common.ControllerBase;

namespace EStore.Web.Controllers
{
    public class ProductsController : ControllerBase
    {
        public ActionResult Index()
        {
            return View("Products/Index");
        }

        [HttpGet]
        public ActionResult Edit(long id = 0)
        {
            var item = Service.Product.Edit(id);
            return View("Products/Edit", item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ProductItem item)
        {
            try
            {
                Service.Product.Save(item);
                return RedirectToRouteNotify("EditProduct", new { id = item.Id });
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex);
                Service.Product.AppendData(item);
                return View("Products/Edit", item);
            }
        }

        [HttpPost]
        public JsonResult Delete(long id)
        {
            Service.Delete<tblProduct>(id);
            return SuccessJsonResult();
        }


        [HttpPost]
        public JsonResult Search(SearchParams @params)
        {
            var items = Service.Product.Search(@params.search.value, @params.OrderBy, @params.IsAsc, @params.length, @params.start);
            return Json(items);
        }

        [HttpPost]
        public JsonResult SearchProductSingle(ProductSingleSearchParams @params)
        {
            var items = Service.ProductSingle.Search(@params.ProductId, @params.search.value, @params.OrderBy, @params.IsAsc, @params.length,
                @params.start);

            foreach (var item in items.data)
                item.View = this.RenderRazorViewToString(item, "~/Views/Shared/Products/ProductSingleGridRow.cshtml");

            return Json(items);
        }

        [HttpPost]
        public JsonResult SaveProductSingle(ProductSingleItem item)
        {
            Service.ProductSingle.Save(item);
            return SuccessJsonResult();
        }

        [HttpPost]
        public JsonResult DeleteProductSingle(long id)
        {
            Service.Delete<tblProductSingle>(id);
            return SuccessJsonResult();
        }
    }
}
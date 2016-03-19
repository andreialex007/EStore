using System.Web;
using System.Web.Mvc;
using EStore.BL.Exceptions;
using EStore.BL.Models;
using EStore.BL.Models.Product;
using EStore.BL.Models._Common;
using EStore.BL.Utils;
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
        public JsonResult UploadImage(HttpPostedFileBase file, long productId, string description)
        {
            var url = CommonUtils.UploadFileToDirectory(file, "Products");
            var item = Service.Product.AddFile(url, description, productId);
            var view = this.RenderRazorViewToString(item, "~/Views/Shared/Products/ImagesGridRow.cshtml");
            return Json(new { view });
        }

        [HttpPost]
        public JsonResult DeleteImage(long id)
        {
            var path = Service.DeleteFile(id);
            CommonUtils.DeleteFile(path);
            return SuccessJsonResult();
        }

        [HttpPost]
        public JsonResult SaveImageDescription(long id, string text)
        {
            Service.Product.SaveImageDescription(id, text);
            return SuccessJsonResult();
        }
    }
}
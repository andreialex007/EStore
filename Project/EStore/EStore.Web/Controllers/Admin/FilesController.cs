using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EStore.BL.Utils;
using EStore.BL.Utils.YandexImages;
using EStore.Web.Code;
using ControllerBase = EStore.Web.Controllers._Common.ControllerBase;

namespace EStore.Web.Controllers.Admin
{
    public class FilesController : ControllerBase
    {
        [HttpPost]
        public JsonResult DeleteFile(long id)
        {
            var path = Service.DeleteFile(id);
            CommonUtils.DeleteFile(path);
            return SuccessJsonResult();
        }

        [HttpPost]
        public JsonResult SaveFileDescription(long id, string text)
        {
            Service.File.SaveFileDescription(id, text);
            return SuccessJsonResult();
        }

        [HttpPost]
        public JsonResult SaveFileFirstPosition(long id)
        {
            Service.File.SaveFileFirstPosition(id);
            return SuccessJsonResult();
        }

        [HttpPost]
        public JsonResult UploadFile(HttpPostedFileBase file, string description, long? productId = null, long? supplierInvoiceId = null)
        {
            var url = CommonUtils.UploadFileToDirectory(file, "Products");
            var item = Service.File.AddFile(url, description, productId, supplierInvoiceId);
            var view = this.RenderRazorViewToString(item,
                productId != null
                    ? "~/Views/Shared/Products/ProductImagesGridRow.cshtml"
                    : "~/Views/Shared/Files/FilesGridRow.cshtml");
            return Json(new { view });
        }

        [HttpPost]
        public ActionResult SearchImages(string term)
        {
            var items = YandexImagesSearcher.Search(term);
            return PartialView("_Common/ImagesSearchResult", items);
        }

        [HttpPost]
        public ActionResult UploadFoundImages(string[] images, long? productId = null)
        {
            var views = new List<string>();
            var paths = images.AsParallel()
                .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                .WithDegreeOfParallelism(16)
                .Select(CommonUtils.DownloadImageAndResize)
                .ToList();

            foreach (var url in paths)
            {
                var item = Service.File.AddFile(url, string.Empty, productId);
                var view = this.RenderRazorViewToString(item, "~/Views/Shared/Products/ProductImagesGridRow.cshtml");
                views.Add(view);
            }

            return Json(new { views });
        }
    }
}
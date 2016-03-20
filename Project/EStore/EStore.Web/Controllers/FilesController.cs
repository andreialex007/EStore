using System.Web;
using System.Web.Mvc;
using EStore.BL.Utils;
using EStore.Web.Code;
using ControllerBase = EStore.Web.Controllers._Common.ControllerBase;

namespace EStore.Web.Controllers
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
        public JsonResult UploadFile(HttpPostedFileBase file, string description, long? productId = null, long? supplierInvoiceId = null)
        {
            var url = CommonUtils.UploadFileToDirectory(file, "Products");
            var item = Service.File.AddFile(url, description, productId, supplierInvoiceId);
            var view = this.RenderRazorViewToString(item, "~/Views/Shared/Files/FilesGridRow.cshtml");
            return Json(new { view });
        }

    }
}
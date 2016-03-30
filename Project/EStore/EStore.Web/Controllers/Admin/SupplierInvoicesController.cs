using System.Web.Mvc;
using EStore.BL.Exceptions;
using EStore.BL.Models.SupplierInvoice;
using EStore.BL.Models._Common;
using EStore.DL.Mapping;
using EStore.Web.Code;
using ControllerBase = EStore.Web.Controllers._Common.ControllerBase;

namespace EStore.Web.Controllers.Admin
{
    public class SupplierInvoicesController : ControllerBase
    {
        public ActionResult Index()
        {
            return View("~/Views/Admin/SupplierInvoices/Index.cshtml");
        }

        [HttpPost]
        public JsonResult Search(SearchParams @params)
        {
            var items = Service.SupplierInvoice.Search(@params.search.value, @params.OrderBy, @params.IsAsc, @params.length, @params.start);
            return Json(items);
        }

        [HttpGet]
        public ActionResult Edit(long id = 0)
        {
            var invoiceItem = Service.SupplierInvoice.Edit(id);
            return View("~/Views/Admin/SupplierInvoices/Edit.cshtml", invoiceItem);
        }

        [HttpPost]
        public ActionResult Edit(SupplierInvoiceItem item)
        {
            try
            {
                Service.SupplierInvoice.Save(item);
                return RedirectToRouteNotify("EditSupplierInvoice", new { id = item.Id });
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex);
                Service.SupplierInvoice.AppendData(item);
                return View("~/Views/Admin/SupplierInvoices/Edit.cshtml", item);
            }
        }

        [HttpPost]
        public JsonResult SaveInvoicePosition(SupplierInvoicePositionItem item)
        {
            Service.InvoicePosition.Save(item);
            var view = this.RenderRazorViewToString(item, "~/Views/Admin/SupplierInvoices/SupplierInvoicePositionsGridRow.cshtml");
            return Json(new { view });
        }

        [HttpPost]
        public JsonResult DeleteInvoicePosition(long id)
        {
            Service.Delete<tblSupplierInvoicePosition>(id);
            return SuccessJsonResult();
        }

        [HttpPost]
        public JsonResult GenerateProductSingles(long id)
        {
            Service.ProductSingle.GenerateProductSingles(id);
            return SuccessJsonResult();
        }
    }
}
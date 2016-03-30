using System.Web.Mvc;
using EStore.BL.Exceptions;
using EStore.BL.Models;
using EStore.DL.Mapping;
using ControllerBase = EStore.Web.Controllers._Common.ControllerBase;

namespace EStore.Web.Controllers.Admin
{
    public class AdminUsersController : ControllerBase
    {
        public ActionResult Index()
        {
            var items = Service.AdminUser.All();
            return View("~/Views/Admin/AdminUsers/Index.cshtml", items);
        }


        [HttpGet]
        public ActionResult Edit(long id = 0)
        {
            var item = Service.AdminUser.Edit(id);
            return View("~/Views/Admin/AdminUsers/Edit.cshtml", item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(AdminUserItem item)
        {
            try
            {
                Service.AdminUser.Save(item);
                return RedirectToRouteNotify("EditAdminUser", new { id = item.Id });
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex);
                return View("~/Views/Admin/AdminUsers/Edit.cshtml", item);
            }
        }

        [HttpPost]
        public JsonResult Delete(long id)
        {
            Service.Delete<tblUser>(id);
            return SuccessJsonResult();
        }
    }
}
using System.Web.Mvc;
using EStore.BL.Exceptions;
using EStore.BL.Models;
using EStore.Web.Code;
using ControllerBase = EStore.Web.Controllers._Common.ControllerBase;

namespace EStore.Web.Controllers.Admin
{
    public class AdminAccountController : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult LogOut()
        {
            this.SignOut();
            return Redirect("~/");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View("~/Views/Admin/AdminAccount/Login.cshtml", new AdminLoginModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(AdminLoginModel model, string returnUrl = "")
        {
            try
            {
                var user = Service.AdminUser.Login(model.Username, model.Password);
                this.SignIn(user, true);
                return Redirect(returnUrl);
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex);
                model.Password = string.Empty;
                return View("~/Views/Admin/AdminAccount/Login.cshtml", model);
            }
        }
    }
}
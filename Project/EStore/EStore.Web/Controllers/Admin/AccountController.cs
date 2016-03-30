using System.Web.Mvc;

namespace EStore.Web.Controllers.Admin
{
    [Authorize]
    public class AccountController : Controller
    {
        public ActionResult LogOut()
        {
            throw new System.NotImplementedException();
        }
    }
}
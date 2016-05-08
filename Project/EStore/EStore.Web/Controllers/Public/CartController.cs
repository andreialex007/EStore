using System.Web.Mvc;
using ControllerBase = EStore.Web.Controllers._Common.ControllerBase;

namespace EStore.Web.Controllers.Public
{
    [AllowAnonymous]
    public class CartController : ControllerBase
    {
        [HttpGet]
        public ActionResult Cart()
        {
            return null;
        }

        [HttpPost]
        public ActionResult AddToCart(long productId)
        {
            Service.Cart.Add(productId);
            return View("~/Views/_Common/ShoppingCartBlock.cshtml");
        }
    }
}
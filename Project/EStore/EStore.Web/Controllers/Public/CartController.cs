using System.Web.Mvc;
using EStore.BL.Models;
using ControllerBase = EStore.Web.Controllers._Common.ControllerBase;

namespace EStore.Web.Controllers.Public
{
    [AllowAnonymous]
    public class CartController : ControllerBase
    {
        [HttpGet]
        public ActionResult Cart()
        {
            var model = Service.Cart.GetCart();
            return View("~/Views/Public/Cart/Index.cshtml", model);
        }

        [HttpPost]
        public ActionResult AddToCart(long productId)
        {
            Service.Cart.Add(productId);
            return PartialView("~/Views/_Common/ShoppingCartBlock.cshtml");
        }

        [HttpPost]
        public ActionResult RemoveFromCart(long productId)
        {
            Service.Cart.Remove(productId);
            return PartialView("~/Views/_Common/ShoppingCartBlock.cshtml");
        }
    }
}
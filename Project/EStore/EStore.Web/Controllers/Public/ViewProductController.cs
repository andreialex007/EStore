using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EStore.BL.Models.Product;
using ControllerBase = EStore.Web.Controllers._Common.ControllerBase;

namespace EStore.Web.Controllers.Public
{
    [AllowAnonymous]
    public class ViewProductController : ControllerBase
    {
        [HttpGet]
        public ActionResult View(long id)
        {
            var productItem = Service.Product.Get(id);
            return View("~/Views/Public/ViewProduct/View.cshtml", productItem);
        }
    }
}
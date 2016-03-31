using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EStore.BL.Models;
using ControllerBase = EStore.Web.Controllers._Common.ControllerBase;

namespace EStore.Web.Controllers.Public
{
    [AllowAnonymous]
    public class PublicArticlesController : ControllerBase
    {
        public ActionResult ViewPage()
        {
            var articleItem = new ArticleItem();
            return View("~/Views/Public/Articles/View.cshtml", articleItem);
        }
    }
}
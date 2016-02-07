using System.Collections.Generic;
using System.Web.Mvc;
using EStore.BL.Models;
using ControllerBase = EStore.Web.Controllers._Common.ControllerBase;

namespace EStore.Web.Controllers
{
    public class ArticlesController : ControllerBase
    {
        public ActionResult Index()
        {
            var items = Service.Article.All();
            return View("Articles/Index", items);
        }

        [HttpGet]
        public ActionResult Edit(long id = 0)
        {
            return Content("Add");
        }
    }
}
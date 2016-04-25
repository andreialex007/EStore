using System.Web.Mvc;
using EStore.BL.Models;
using ControllerBase = EStore.Web.Controllers._Common.ControllerBase;

namespace EStore.Web.Controllers.Public
{
    [AllowAnonymous]
    public class PublicArticlesController : ControllerBase
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Views/Public/Articles/View.cshtml", new PublicArticleItem
            {
                Title = "Главная"
            });
        }

        [HttpGet]
        public ActionResult Delivery()
        {
            var item = Service.Article.Get(2);
            item.PageSmallTitle = "Информация о доставке";
            return View("~/Views/Public/Articles/View.cshtml", item);
        }

        [HttpGet]
        public ActionResult Guarantee()
        {
            var item = Service.Article.Get(3);
            item.PageSmallTitle = "Информация о гарантии";
            return View("~/Views/Public/Articles/View.cshtml", item);
        }

        [HttpGet]
        public ActionResult Contacts()
        {
            var item = Service.Article.Get(4);
            item.PageSmallTitle = "Информация о гарантии";
            return View("~/Views/Public/Articles/View.cshtml", item);
        }
    }
}
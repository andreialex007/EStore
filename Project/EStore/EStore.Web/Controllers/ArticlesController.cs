using System.Web.Mvc;
using EStore.BL.Exceptions;
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
            var articleItem = Service.Article.Edit(id);
            return View("Articles/Edit", articleItem);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ArticleItem item)
        {
            try
            {
                Service.Article.Save(item);
                return RedirectToRouteNotify("EditArticle", new { id = item.Id });
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex);
                Service.Article.AppendData(item);
                return View("Articles/Edit", item);
            }
        }
    }
}
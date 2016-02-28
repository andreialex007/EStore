using System.Web.Mvc;
using System.Web.Routing;

namespace EStore.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.LowercaseUrls = true;

            routes.MapRoute("AddArticle", "Articles/add", new { controller = "Articles", action = "Edit" });
            routes.MapRoute("EditArticle", "Articles/{id}",
                new { controller = "Articles", action = "Edit" },
                new { id = @"\d+" });

            routes.MapRoute("AddProduct", "Products/add", new { controller = "Products", action = "Edit" });
            routes.MapRoute("EditProduct", "Products/{id}",
                new { controller = "Products", action = "Edit" },
                new { id = @"\d+" });

            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });

        }
    }
}
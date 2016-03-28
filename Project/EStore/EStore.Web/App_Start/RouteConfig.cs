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

            routes.MapRoute("AddArticle", "admin/articles/add", new { controller = "Articles", action = "Edit" });
            routes.MapRoute("EditArticle", "admin/articles/{id}",
                new { controller = "Articles", action = "Edit" },
                new { id = @"\d+" });

            routes.MapRoute("AddProductCategory", "admin/productcategories/add", new { controller = "ProductCategories", action = "Edit" });
            routes.MapRoute("EditProductCategory", "admin/productcategories/{id}",
                new { controller = "ProductCategories", action = "Edit" },
                new { id = @"\d+" });

            routes.MapRoute("AddProduct", "admin/products/add", new { controller = "Products", action = "Edit" });
            routes.MapRoute("EditProduct", "admin/products/{id}",
                new { controller = "Products", action = "Edit" },
                new { id = @"\d+" });

            routes.MapRoute("AddSupplierInvoice", "admin/supplierinvoices/add", new { controller = "SupplierInvoices", action = "Edit" });
            routes.MapRoute("EditSupplierInvoice", "admin/supplierinvoices/{id}",
                new { controller = "SupplierInvoices", action = "Edit" },
                new { id = @"\d+" });

            routes.MapRoute("Default", "admin/{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });

        }
    }
}
using System.Web.Mvc;
using System.Web.Routing;

namespace EStore.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            RegisterAdminRoutes(routes);

            RegisterPublicRoutes(routes);
        }

        private static void RegisterPublicRoutes(RouteCollection routes)
        {
            routes.MapRoute(null, "delivery", new { controller = "PublicArticles", action = "Delivery" });
            routes.MapRoute(null, "guarantee", new { controller = "PublicArticles", action = "Guarantee" });
            routes.MapRoute(null, "contacts", new { controller = "PublicArticles", action = "Contacts" });
            routes.MapRoute(null, "catalog", new { controller = "Catalog", action = "Index" });
            routes.MapRoute(null, "catalog/{id}", new { controller = "Catalog", action = "Catalog" }, new { id = @"\d+" });
            routes.MapRoute("ViewProduct", "catalog/product{id}", new { controller = "ViewProduct", action = "View" }, new { id = @"\d+" });
            routes.MapRoute(null, "{controller}/{action}/{id}", new { controller = "PublicArticles", action = "Index", id = UrlParameter.Optional });
        }

        private static void RegisterAdminRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.LowercaseUrls = true;

            routes.MapRoute(null, "admin/login", new { controller = "AdminAccount", action = "Login" });
            routes.MapRoute(null, "admin", new { controller = "Dashboard", action = "Index" });

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


            routes.MapRoute("AddAdminUser", "admin/adminusers/add", new { controller = "AdminUsers", action = "Edit" });
            routes.MapRoute("EditAdminUser", "admin/adminusers/{id}",
                new { controller = "AdminUsers", action = "Edit" },
                new { id = @"\d+" });

            routes.MapRoute("AddSupplierInvoice", "admin/supplierinvoices/add", new { controller = "SupplierInvoices", action = "Edit" });
            routes.MapRoute("EditSupplierInvoice", "admin/supplierinvoices/{id}",
                new { controller = "SupplierInvoices", action = "Edit" },
                new { id = @"\d+" });

            routes.MapRoute("Default", "admin/{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}
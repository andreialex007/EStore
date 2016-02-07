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

            routes.MapRoute("AddCustomer", "customers/add", new { controller = "Customers", action = "Edit" });
            routes.MapRoute("EditCustomer", "customers/{id}",
                new { controller = "Customers", action = "Edit" },
                new { id = @"\d+" });


            routes.MapRoute("AddTicket", "Tickets/add", new { controller = "Tickets", action = "Edit" });
            routes.MapRoute("EditTicket", "Tickets/{id}",
                new { controller = "Tickets", action = "Edit" },
                new { id = @"\d+" });

            routes.MapRoute(
                "SendInvoiceEmail",
                "tickets/{ticketId}/invoice/{id}/email",
                new { controller = "Invoice", action = "Email" },
                new { ticketId = @"\d+", id = @"\d+" });


            routes.MapRoute(
                "SendTicketEmail",
                "tickets/{id}/sendemail",
                new { controller = "Tickets", action = "SendEmail" }, new { id = @"\d+" });

            routes.MapRoute(
                "AddInvoice",
                "tickets/{ticketId}/invoice/add",
                new { controller = "Invoice", action = "Edit" }, new { ticketId = @"\d+" });

            routes.MapRoute(
                "StopNotification",
                "Notification/StopNotification/{notificationId}/{ticketId}",
                new { controller = "Notification", action = "StopNotification" },
                new { notificationId = @"\d+", ticketId = @"\d+" });

            routes.MapRoute(
                "EditInvoice",
                "tickets/{ticketId}/invoice/{id}",
                new { controller = "Invoice", action = "Edit" }, new { ticketId = @"\d+", id = @"\d+" });

            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}
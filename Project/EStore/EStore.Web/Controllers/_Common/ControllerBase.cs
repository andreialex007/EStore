using System.Web.Mvc;

namespace EStore.Web.Controllers._Common
{
    [Authorize]
    public class ControllerBase : Controller
    {
//        protected AppService Service;
//
//        public ControllerBase()
//        {
//            ActionInvoker = new ExceptionControllerActionInvoker();
//            Service = new AppService(new AppEntities());
//        }
//
//        protected override JsonResult Json(object data, string contentType,
//            Encoding contentEncoding, JsonRequestBehavior behavior)
//        {
//            return new JsonNetResult
//                   {
//                       Data = data,
//                       ContentType = contentType,
//                       ContentEncoding = contentEncoding,
//                       JsonRequestBehavior = behavior
//                   };
//        }
//
//        protected JsonResult Json(object data, string contentType,
//            Encoding contentEncoding, JsonRequestBehavior behavior, JsonSerializerSettings settings)
//        {
//            var result = new JsonNetResult
//                         {
//                             Data = data,
//                             ContentType = contentType,
//                             ContentEncoding = contentEncoding,
//                             JsonRequestBehavior = behavior
//                         };
//            if (settings != null)
//                result.JsonSerializerSettings = settings;
//
//            return result;
//        }
//
//        protected RedirectToRouteResult RedirectToRouteNotify(string routeName, object routeValues)
//        {
//            var cookie = new HttpCookie("Saved", "Saved") { Path = "/" };
//            Response.Cookies.Add(cookie);
//            return RedirectToRoute(routeName, routeValues);
//        }
//
//        protected JsonResult SuccessJsonResult()
//        {
//            return Json(new { Result = "ok" });
//        }
//
//        protected void AddModelErrors(ValidationException exception)
//        {
//            ModelState.Clear();
//            exception.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));
//        }
//
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                Service.Dispose();
//            }
//            base.Dispose(disposing);
//        }

    }
}
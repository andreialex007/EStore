using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace EStore.Web.Code
{
    public class ExceptionControllerActionInvoker : ControllerActionInvoker
    {
        protected override ActionResult InvokeActionMethod(ControllerContext controllerContext, ActionDescriptor actionDescriptor, IDictionary<string, object> parameters)
        {
            if (!HttpContext.Current.Request.IsAjaxRequest())
                return base.InvokeActionMethod(controllerContext, actionDescriptor, parameters);

            try
            {
                return base.InvokeActionMethod(controllerContext, actionDescriptor, parameters);
            }
            catch (ValidationException ex)
            {
                return new JsonResult
                {
                    Data = new
                    {
//                        ex.Errors,
                        HasErrors = true
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
    }
}
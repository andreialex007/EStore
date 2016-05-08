﻿using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoParse;
using Newtonsoft.Json;

namespace EStore.Web.Code
{
    public static class Extensions
    {
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            var isAjaxRequest = new HttpRequestWrapper(HttpContext.Current.Request).IsAjaxRequest();
            return isAjaxRequest;
        }

        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static string ErrorsToString(this ModelErrorCollection collection)
        {
            return string.Join(".", collection.Select(x => x.ErrorMessage));
        }

        public static string RenderRazorViewToString(this ControllerBase controller, object model, string viewName)
        {
            controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        public static string GetReferrer(this ControllerBase controller)
        {
            var request = HttpContext.Current.Request;
            return request.UrlReferrer != null
                ? request.UrlReferrer.PathAndQuery
                : string.Empty;
        }

        public static string ContentFullPath(this UrlHelper url, string virtualPath)
        {
            var requestUrl = url.RequestContext.HttpContext.Request.Url;
            var result = $"{requestUrl.Scheme}://{requestUrl.Authority}{VirtualPathUtility.ToAbsolute(virtualPath)}";
            return result;
        }

        public static string GetProductsForm(int products)
        {
            if (products > 20)
                products = products.ToString().GetLast(1).TryParse<int>();

            if (products == 1)
                return "Товар";

            if (products > 1 && products < 5)
                return "Товара";

            return "Товаров";
        }

        public static string GetLast(this string source, int tail_length)
        {
            if (tail_length >= source.Length)
                return source;
            return source.Substring(source.Length - tail_length);
        }

    }
}
using System;
using System.IO;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace EStore.Web.Code
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString EmbedCss(this HtmlHelper htmlHelper, string path)
        {
            var cssFilePath = HttpContext.Current.Server.MapPath(path);
            try
            {
                var cssText = File.ReadAllText(cssFilePath);
                var styleElement = new TagBuilder("style");
                styleElement.SetInnerText(cssText);
                return MvcHtmlString.Create(styleElement.ToString());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static MvcHtmlString ImageBase64Src(this HtmlHelper html, byte[] image)
        {
            var img = $"data:image/jpg;base64,{Convert.ToBase64String(image)}";
            return new MvcHtmlString(img);
        }

        public static MvcHtmlString ImageBase64Src(this HtmlHelper html, string url)
        {
            var path = HostingEnvironment.MapPath(url);
            var img = $"data:image/jpg;base64,{Convert.ToBase64String(File.ReadAllBytes(path))}";
            return new MvcHtmlString(img);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoParse;
using EStore.BL.Models.Product;
using HtmlAgilityPack;
using ScrapySharp.Extensions;

namespace EStore.BL.Utils.ProductFeedbackParsers
{
    //    public static class OtzovikParser
    //    {
    //        public static List<ProductFeedbackItem> ParseFeedbacks(string url)
    //        {
    //            return PhantomJsUtils.Process(driver =>
    //            {
    //                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(2));
    //                driver.Navigate().GoToUrl(url);
    //                var pageSource = driver.PageSource;
    //                var docuemnt = new HtmlDocument();
    //                docuemnt.LoadHtml(pageSource);
    //
    //                var productFeedbackItems = new List<ProductFeedbackItem>();
    //               
    //
    //                return productFeedbackItems;
    //            });
    //        }
    //    }
}

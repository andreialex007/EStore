using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EStore.BL.Models;
using HtmlAgilityPack;
using ScrapySharp.Extensions;

namespace EStore.BL.Utils
{
    public class YandexMarketSpecsParser
    {
        public static List<SpecItem> Parse(string url)
        {
            return PhantomJsUtils.Process(driver =>
            {
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(2));
                driver.Navigate().GoToUrl(url);
                var pageSource = driver.PageSource;
                var docuemnt = new HtmlDocument();
                docuemnt.LoadHtml(pageSource);

                var items = new List<SpecItem>();

                var specNames = docuemnt.DocumentNode.CssSelect(".product-spec-wrap .product-spec__name-inner");
                var specValues = docuemnt.DocumentNode.CssSelect(".product-spec-wrap .product-spec__value-inner");

                for (int i = 0; i < specNames.Count(); i++)
                {
                    var specName = specNames.ElementAt(i);
                    var name = specName.InnerHtml.Split("<".ToCharArray()).First();
                    var specValue = specValues.ElementAt(i);
                    var value = HttpUtility.HtmlDecode(specValue.InnerHtml).Trim();

                    var item = new SpecItem
                    {
                        Name = name,
                        Value = value
                    };

                    items.Add(item);
                }

                return items;
            });
        }
    }
}

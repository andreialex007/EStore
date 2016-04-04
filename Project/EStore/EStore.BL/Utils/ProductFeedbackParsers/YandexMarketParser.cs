using System;
using System.Collections.Generic;
using System.Linq;
using AutoParse;
using EStore.BL.Models.Product;
using HtmlAgilityPack;
using ScrapySharp.Extensions;

namespace EStore.BL.Utils.ProductFeedbackParsers
{
    public static class YandexMarketParser
    {
        public static List<ProductFeedbackItem> ParseFeedbacks(string url)
        {
            return PhantomJsUtils.Process(driver =>
            {
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(2));
                driver.Navigate().GoToUrl(url);
                var pageSource = driver.PageSource;
                var docuemnt = new HtmlDocument();
                docuemnt.LoadHtml(pageSource);

                var productFeedbackItems = new List<ProductFeedbackItem>();
                foreach (var reviewItem in docuemnt.DocumentNode.CssSelect(".product-review-item").ToList())
                {
                    ProductFeedbackItem productFeedbackItem = null;
                    try
                    {
                        var rating = reviewItem.CssSelect("div.rating > meta").Single().Attributes["content"].Value.TryParseNullable<decimal>();
                        var user = reviewItem.CssSelect(".product-review-user a").FirstOrDefault()?.InnerText ?? "Анонимно";
                        var pluses = reviewItem.CssSelect(".product-review-item__stat").Skip(1).First().CssSelect(".product-review-item__text").First().InnerText;
                        var minuses = reviewItem.CssSelect(".product-review-item__stat").Skip(2).First().CssSelect(".product-review-item__text").First().InnerText;
                        var comment = reviewItem.CssSelect("meta[itemprop='description']").Single().Attributes["content"].Value.ToString();

                        productFeedbackItem = new ProductFeedbackItem
                        {
                            Stars = (int?)Math.Round(rating ?? 0),
                            Comment = comment,
                            Minuses = minuses,
                            Pluses = pluses,
                            UserName = user
                        };

                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    productFeedbackItems.Add(productFeedbackItem);
                }

                return productFeedbackItems;
            });
        }
    }
}
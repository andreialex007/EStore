using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EStore.BL.Utils.YandexImages.Dto;
using HtmlAgilityPack;
using Newtonsoft.Json;
using ScrapySharp.Extensions;

namespace EStore.BL.Utils.YandexImages
{
    public static class YandexImagesSearcher
    {
        public static List<ImageSearchItem> Search(string searchTerm)
        {
            return PhantomJsUtils.Process(driver =>
            {
                try
                {
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
                    driver.Navigate().GoToUrl($"https://yandex.ru/images/search?text={HttpUtility.UrlEncode(searchTerm)}&isize=large");
                    var pageSource = driver.PageSource;

                    var docuemnt = new HtmlDocument();
                    docuemnt.LoadHtml(pageSource);

                    var images = docuemnt.DocumentNode.CssSelect(".serp-list .serp-item")
                        .Select(x => HttpUtility.HtmlDecode(x.Attributes["data-bem"].Value))
                        .Select(JsonConvert.DeserializeObject<YandexImageItem>).Select(x => new ImageSearchItem
                        {
                            Original = x.SerpItem.img_href,
                            Preview = x.SerpItem.thumb.url
                        })
                        .ToList();

                    return images;
                }
                catch (Exception ex)
                {
                    return new List<ImageSearchItem>();
                }
            });
        }
    }
}
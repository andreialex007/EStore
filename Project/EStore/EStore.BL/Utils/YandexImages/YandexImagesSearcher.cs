using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EStore.BL.Utils.YandexImages.Dto;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace EStore.BL.Utils.YandexImages
{
    public static class YandexImagesSearcher
    {
        public static List<ImageSearchItem> Search(string searchTerm)
        {
            return PhantomJsUtils.Process(driver =>
            {
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(2));
                driver.Navigate().GoToUrl($"https://yandex.ru/images/search?text={HttpUtility.UrlEncode(searchTerm)}&isize=large");
                var pageSource = driver.PageSource;

                var docuemnt = new HtmlDocument();
                docuemnt.LoadHtml(pageSource);

                var images = docuemnt.DocumentNode
                    .SelectNodes("//*[contains(@class,'serp-item serp-item_type_search serp-item_group_search')]")
                    .Select(x => HttpUtility.HtmlDecode(x.Attributes["data-bem"].Value))
                    .Select(JsonConvert.DeserializeObject<YandexImageItem>).Select(x => new ImageSearchItem
                    {
                        Original = x.SerpItem.img_href,
                        Preview = x.SerpItem.thumb.url
                    })
                    .ToList();

                return images;
            });
        }
    }
}
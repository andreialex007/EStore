using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using EStore.BL.Utils.YandexImages.Dto;
using HtmlAgilityPack;
using Newtonsoft.Json;
using OpenQA.Selenium.PhantomJS;

namespace EStore.BL.Utils.YandexImages
{
    public static class YandexImagesSearcher
    {
        private const string SearchUrl = "https://yandex.ru/images/search?text={0}&isize=large";
        private static PhantomJSDriver _phantomJsDriver;
        private static readonly object _locker = new object();

        public static List<ImageSearchItem> Search(string searchTerm)
        {
            lock (_locker)
            {
                if (_phantomJsDriver == null)
                {
                    foreach (var process in Process.GetProcessesByName("phantomjs"))
                        process.Kill();
                    _phantomJsDriver = new PhantomJSDriver(HttpContext.Current.Request.PhysicalApplicationPath + "\\Resources");
                }

                var pageSource = string.Empty;
                _phantomJsDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(2));
                _phantomJsDriver.Navigate().GoToUrl(string.Format(SearchUrl, HttpUtility.UrlEncode(searchTerm)));
                pageSource = _phantomJsDriver.PageSource;

                var docuemnt = new HtmlDocument();
                docuemnt.LoadHtml(pageSource);

                var images = Enumerable.Select(docuemnt.DocumentNode
                    .SelectNodes("//*[contains(@class,'serp-item serp-item_type_search serp-item_group_search')]")
                    .Select(x => HttpUtility.HtmlDecode(x.Attributes["data-bem"].Value))
                    .Select(JsonConvert.DeserializeObject<YandexImageItem>), x => new ImageSearchItem
                    {
                        Original = x.SerpItem.img_href,
                        Preview = x.SerpItem.thumb.url
                    })
                    .ToList();

                return images;
            }
        }
    }
}
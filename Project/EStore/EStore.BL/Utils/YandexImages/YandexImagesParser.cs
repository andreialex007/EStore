using System;
using System.Linq;
using EStore.BL.Utils.YandexImages.Dto;
using Newtonsoft.Json;
using ScrapySharp.Extensions;
using ScrapySharp.Network;

namespace EStore.BL.Utils.YandexImages
{
    public static class YandexImagesParser
    {
        public const string SearchUrl = "https://yandex.ru/images/search?text={0}&isize=large";

        public static void Parse(string searchTerm)
        {
            var browser = new ScrapingBrowser();
            var page = browser.NavigateToPage(new Uri(string.Format(SearchUrl, searchTerm)));
            var images = page.Html.CssSelect(".serp-item")
                .Select(x => x.Attributes["data-bem"].Value)
                .Select(JsonConvert.DeserializeObject<YandexImageItem>)
                .Select(x=>x.SerpItem.img_href)
                .ToList();
        }

    }
}

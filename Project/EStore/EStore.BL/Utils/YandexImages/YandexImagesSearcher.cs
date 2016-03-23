using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using EStore.BL.Utils.YandexImages.Dto;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace EStore.BL.Utils.YandexImages
{
    public static class YandexImagesSearcher
    {
        private const string SearchUrl = "https://yandex.ru/images/search?text={0}&isize=large";

        public static List<ImageSearchItem> Search(string searchTerm)
        {
            var result = string.Empty;
            using (var client = new WebClient())
            {
                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                var downloadData = client.DownloadData(new Uri(string.Format(SearchUrl, searchTerm)));
                result = Encoding.UTF8.GetString(downloadData);
            }

            var docuemnt = new HtmlDocument();
            docuemnt.LoadHtml(result);

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
        }
    }
}
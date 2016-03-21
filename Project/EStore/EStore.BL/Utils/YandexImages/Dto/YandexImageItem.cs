using Newtonsoft.Json;

namespace EStore.BL.Utils.YandexImages.Dto
{
    public class YandexImageItem
    {
        [JsonProperty(PropertyName = "serp-item")]
        public SerpItem SerpItem { get; set; }
    }
}
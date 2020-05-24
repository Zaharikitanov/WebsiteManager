using Newtonsoft.Json;

namespace WebsiteManager.Models.View
{
    public class CreateNewWebsiteData
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string URL { get; set; }

        [JsonProperty("category")]
        public WebsiteCategories Category { get; set; }

        [JsonProperty("homepageSnapshot")]
        public string HomepageSnapshot { get; set; }

        [JsonProperty("loginDetails")]
        public LoginDetails LoginDetails { get; set; }
    }
}

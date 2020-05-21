using Newtonsoft.Json;
using System;

namespace WebsiteManager.Models.View
{
    public class WebsiteViewData
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string URL { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("homepageSnapshot")]
        public string HomepageSnapshot { get; set; }

        [JsonProperty("loginDetails")]
        public LoginModel LoginDetails { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("editedAt")]
        public string EditedAt { get; set; }
    }
}

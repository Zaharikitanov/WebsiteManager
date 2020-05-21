using Newtonsoft.Json;
using System;

namespace WebsiteManager.Models.View
{
    public class WebsiteViewData : CreateNewWebsiteData
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("editedAt")]
        public string EditedAt { get; set; }
    }
}
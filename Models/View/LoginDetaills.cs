using Newtonsoft.Json;

namespace WebsiteManager.Models.View
{
    public class LoginDetaills
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
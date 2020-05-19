using System;

namespace WebsiteManager.Models.View
{
    public class WebsiteViewData
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string URL { get; set; }

        public string Category { get; set; }

        public string HomepageSnapshot { get; set; }

        public LoginModel LoginDetails { get; set; }

        public bool IsDeleted { get; set; }

        public string CreatedAt { get; set; }

        public string EditedAt { get; set; }
    }
}

namespace WebsiteManager.Models.Database
{
    public class Website : Entity
    {
        public string Name { get; set; }

        public string URL { get; set; }

        public WebsiteCategories Category { get; set; }

        public string HomepageSnapshot { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsDeleted { get; set; }

        public string CreatedAt { get; set; }

        public string EditedAt { get; set; }
    }
}

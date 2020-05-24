using System;
using WebsiteManager.Factories.Interfaces;
using WebsiteManager.Helpers.Interfaces;
using WebsiteManager.Models.Database;
using WebsiteManager.Models.View;

namespace WebsiteManager.Factories
{
    public class WebsiteFactory : IWebsiteFactory
    {
        private IStringHash _stringHash;

        public WebsiteFactory(IStringHash stringHash)
        {
            _stringHash = stringHash;
        }

        public Website Create(CreateNewWebsiteData viewData)
        {
            return new Website
            {
                Name = viewData.Name,
                URL = viewData.URL,
                Category = viewData.Category,
                HomepageSnapshot = viewData.HomepageSnapshot,
                Email = viewData.LoginDetails.Email,
                Password = _stringHash.ComputeSha256Hash(viewData.LoginDetails.Password),
                IsDeleted = false,
                CreatedAt = DateTime.Now.ToString()
            };
        }
    }
}

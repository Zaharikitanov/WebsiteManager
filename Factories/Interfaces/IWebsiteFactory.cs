using WebsiteManager.Models.Database;
using WebsiteManager.Models.View;

namespace WebsiteManager.Factories.Interfaces
{
    public interface IWebsiteFactory
    {
        Website Create(CreateNewWebsiteData viewData);
    }
}
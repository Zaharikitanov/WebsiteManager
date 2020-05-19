using WebsiteManager.Models.Data;
using WebsiteManager.Models.View;

namespace WebsiteManager.Factories.Interfaces
{
    public interface IWebsiteFactory
    {
        Website Create(WebsiteViewData viewData);
    }
}
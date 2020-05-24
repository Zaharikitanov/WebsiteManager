using WebsiteManager.Models.Database;
using WebsiteManager.Models.View;

namespace WebsiteManager.Mappers.Interfaces
{
    public interface IWebsiteDataMapper
    {
        WebsiteViewData Map(Website website);
    }
}
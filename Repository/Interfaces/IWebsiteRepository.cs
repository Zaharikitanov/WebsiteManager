using System;
using System.Threading.Tasks;
using WebsiteManager.Models.View;
using WebsiteManager.Repository.Interfaces;

namespace WebsiteManager.Repository.Interfaces
{
    public interface IWebsiteRepository: IBaseRepository
    {
        Task<WebsiteViewData> GetEntityDetailsAsync(Guid entityId);
    }
}
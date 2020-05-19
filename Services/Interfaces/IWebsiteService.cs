using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteManager.Models.Data;
using WebsiteManager.Models.View;

namespace WebsiteManager.Services.Interfaces
{
    public interface IWebsiteService
    {
        Task CreateEntityAsync(WebsiteViewData viewData);
        Task<Website> GetEntityByIdAsync(Guid entityId);
        Task<List<Website>> GetEntitiesListAsync();
        Task SoftDeleteEntityAsync(Guid entityId);
        Task UpdateEntityAsync(WebsiteViewData viewData);
    }
}
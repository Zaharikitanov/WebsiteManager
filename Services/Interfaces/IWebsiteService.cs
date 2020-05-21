using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteManager.Models.Data;
using WebsiteManager.Models.Outcomes;
using WebsiteManager.Models.View;

namespace WebsiteManager.Services.Interfaces
{
    public interface IWebsiteService
    {
        Task CreateEntityAsync(WebsiteViewData viewData);
        Task<Website> GetEntityByIdAsync(Guid entityId);
        Task<List<Website>> GetEntitiesListAsync();
        Task<UpdateEntityOutcome> SoftDeleteEntityAsync(Guid entityId);
        Task<UpdateEntityOutcome> UpdateEntityAsync(WebsiteViewData viewData);
    }
}
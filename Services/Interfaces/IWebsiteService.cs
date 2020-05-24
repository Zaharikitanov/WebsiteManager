using EntityFrameworkPaginateCore;
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
        Task<CreateEntityOutcome> CreateEntityAsync(CreateNewWebsiteData viewData);
        Task<Website> GetEntityByIdAsync(Guid entityId);
        Task<List<WebsiteViewData>> GetEntitiesListAsync();
        Task<UpdateEntityOutcome> SoftDeleteEntityAsync(Guid entityId);
        Task<UpdateEntityOutcome> UpdateEntityAsync(WebsiteViewData viewData);

        Task<Page<Website>> GetPaginatedEntitiesAsync(int pageSize, int currentPage, string searchText, int sortBy);
    }
}
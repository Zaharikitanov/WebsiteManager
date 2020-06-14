using EntityFrameworkPaginateCore;
using System;
using System.Threading.Tasks;
using WebsiteManager.Models;
using WebsiteManager.Models.View;

namespace WebsiteManager.Services.Interfaces
{
    public interface IWebsiteService
    {
        Task<EntityActionOutcome> CreateEntityAsync(WebsiteInputData viewData);

        Task<WebsiteViewData> GetEntityByIdAsync(Guid entityId);

        Task<EntityActionOutcome> SoftDeleteEntityAsync(Guid entityId);

        Task<EntityActionOutcome> UpdateEntityAsync(WebsiteInputData viewData, Guid id);

        Task<Page<WebsiteViewData>> GetPaginatedEntitiesAsync(int pageSize, int currentPage, string searchText, SortByOptions sortBy);
    }
}
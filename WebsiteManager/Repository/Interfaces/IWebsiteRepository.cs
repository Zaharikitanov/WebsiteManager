using EntityFrameworkPaginateCore;
using System;
using System.Threading.Tasks;
using WebsiteManager.Models;
using WebsiteManager.Models.View;

namespace WebsiteManager.Repository.Interfaces
{
    public interface IWebsiteRepository: IBaseRepository
    {
        Task<Page<WebsiteViewData>> GetPaginatedResultsAsync(int pageSize, int currentPage, string searchText, SortByOptions sortBy);

        Task<WebsiteViewData> GetByIdAsync(Guid entityId);
    }
}
using EntityFrameworkPaginateCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteManager.Models;
using WebsiteManager.Models.Database;
using WebsiteManager.Models.View;

namespace WebsiteManager.Repository.Interfaces
{
    public interface IWebsiteRepository: IBaseRepository
    {
        Task<List<WebsiteViewData>> GetNotDeletedEntitiesAsync();

        Task<Page<WebsiteViewData>> GetPaginatedResultsAsync(int pageSize, int currentPage, string searchText, SortByOptions sortBy);
    }
}
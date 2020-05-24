using EntityFrameworkPaginateCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteManager.Models.Data;
using WebsiteManager.Models.View;

namespace WebsiteManager.Repository.Interfaces
{
    public interface IWebsiteRepository: IBaseRepository
    {
        Task<List<WebsiteViewData>> GetNotDeletedEntitiesAsync();

        Task<Page<Website>> GetPaginatedResultsAsync(int pageSize, int currentPage, string searchText, int sortBy);
    }
}
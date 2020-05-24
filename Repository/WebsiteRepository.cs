using EntityFrameworkPaginateCore;
using System.Linq;
using System.Threading.Tasks;
using WebsiteManager.DatabaseContext;
using WebsiteManager.Mappers.Interfaces;
using WebsiteManager.Models;
using WebsiteManager.Models.View;
using WebsiteManager.Repository.Interfaces;

namespace WebsiteManager.Repository
{
    public class WebsiteRepository : BaseRepository, IWebsiteRepository
    {
        private IWebsiteDataMapper _mapper;

        public WebsiteRepository(WebsiteManagerContext dbContext, IWebsiteDataMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Page<WebsiteViewData>> GetPaginatedResultsAsync(int pageSize, int currentPage, string searchText, SortByOptions sortBy)
        {
            var filters = new Filters<WebsiteViewData>();
            filters.Add(!string.IsNullOrEmpty(searchText), x => x.Name.Contains(searchText));

            var sorts = new Sorts<WebsiteViewData>();
            sorts.Add(sortBy == SortByOptions.Name, x => x.Name);
            sorts.Add(sortBy == SortByOptions.CreatedAt, x => x.CreatedAt);
            sorts.Add(sortBy == SortByOptions.EditedAt, x => x.EditedAt);
            
            return await _dbContext.Websites
                .Where(w => w.IsDeleted == false)
                .Select(e => _mapper.Map(e))
                .PaginateAsync(currentPage, pageSize, sorts, filters);
        }
    }
}

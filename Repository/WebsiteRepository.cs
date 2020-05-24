using EntityFrameworkPaginateCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebsiteManager.DatabaseContext;
using WebsiteManager.Mappers.Interfaces;
using WebsiteManager.Models;
using WebsiteManager.Models.Database;
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

        public async Task<List<WebsiteViewData>> GetNotDeletedEntitiesAsync()
        {
            var entities = await _dbContext.Websites.Where(w => w.IsDeleted == false)
                .Select(w => new WebsiteViewData
                {
                    Id = w.Id,
                    CreatedAt = w.CreatedAt,
                    EditedAt = w.EditedAt,
                    Name = w.Name,
                    URL = w.URL,
                    Category = w.Category,
                    HomepageSnapshot = w.HomepageSnapshot,
                    LoginDetails = new LoginDetails {
                        Email = w.Email,
                        Password = w.Password
                    }
                })
                .ToListAsync();

            return entities;
        }

        public async Task<Page<WebsiteViewData>> GetPaginatedResultsAsync(int pageSize, int currentPage, string searchText, SortByOptions sortBy)
        {
            var filters = new Filters<WebsiteViewData>();
            filters.Add(!string.IsNullOrEmpty(searchText), x => x.Name.Contains(searchText));

            var sorts = new Sorts<WebsiteViewData>();
            sorts.Add(sortBy == SortByOptions.Name, x => x.Name);
            sorts.Add(sortBy == SortByOptions.CreatedAt, x => x.CreatedAt);
            sorts.Add(sortBy == SortByOptions.EditedAt, x => x.EditedAt);
            
            return await _dbContext.Websites.Select(e => _mapper.Map(e)).PaginateAsync<WebsiteViewData>(currentPage, pageSize, sorts, filters);
        }
    }
}

using EntityFrameworkPaginateCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebsiteManager.DatabaseContext;
using WebsiteManager.Models.Data;
using WebsiteManager.Models.View;
using WebsiteManager.Repository.Interfaces;

namespace WebsiteManager.Repository
{
    public class WebsiteRepository : BaseRepository, IWebsiteRepository
    {
        public WebsiteRepository(WebsiteManagerContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
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
                    LoginDetails = new LoginDetaills {
                        Email = w.Email,
                        Password = w.Password
                    }
                })
                .ToListAsync();

            return entities;
        }

        public async Task<Page<Website>> GetPaginatedResultsAsync(int pageSize, int currentPage, string searchText, int sortBy)
        {
            var filters = new Filters<Website>();
            filters.Add(!string.IsNullOrEmpty(searchText), x => x.Name.Contains(searchText));

            var sorts = new Sorts<Website>();
            sorts.Add(sortBy == 1, x => x.Name);
            sorts.Add(sortBy == 2, x => x.CreatedAt);
            sorts.Add(sortBy == 3, x => x.EditedAt);

            try
            {
                return await _dbContext.Websites.PaginateAsync(currentPage, pageSize, sorts, filters);
            }
            catch (Exception ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
        }
    }
}

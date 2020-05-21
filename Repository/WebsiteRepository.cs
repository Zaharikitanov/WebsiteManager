using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebsiteManager.DatabaseContext;
using WebsiteManager.Models.Data;
using WebsiteManager.Repository.Interfaces;

namespace WebsiteManager.Repository
{
    public class WebsiteRepository : BaseRepository, IWebsiteRepository
    {
        public WebsiteRepository(WebsiteManagerContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Website>> GetNotDeletedEntitiesAsync()
        {
            var entities = await _dbContext.Websites.Where(c => c.IsDeleted == false).ToListAsync();

            return entities;
        }
    }
}

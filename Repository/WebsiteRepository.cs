using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebsiteManager.DatabaseContext;
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

        public async Task<WebsiteViewData> GetEntityDetailsAsync(Guid entityId)
        {
            var entity = await _dbContext.Websites.Where(c => c.Id == entityId)
                .Select(c => new WebsiteViewData
                {
                    Name = c.Name

                }).SingleOrDefaultAsync();

            return entity;
        }
    }
}

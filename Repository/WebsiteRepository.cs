using WebsiteManager.DatabaseContext;

namespace WebsiteManager.Repository
{
    public class WebsiteRepository : BaseRepository
    {
        public WebsiteRepository(WebsiteManagerContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


    }
}

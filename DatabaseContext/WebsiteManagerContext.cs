using Microsoft.EntityFrameworkCore;

namespace WebsiteManager.DatabaseContext
{
    public class WebsiteManagerContext : DbContext
    {
        public WebsiteManagerContext(DbContextOptions<WebsiteManagerContext> context) : base(context)
        {

        }


    }
}

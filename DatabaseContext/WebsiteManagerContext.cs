using Microsoft.EntityFrameworkCore;
using WebsiteManager.Models.Database;

namespace WebsiteManager.DatabaseContext
{
    public class WebsiteManagerContext : DbContext
    {
        public WebsiteManagerContext(DbContextOptions<WebsiteManagerContext> context) : base(context)
        {

        }

        public DbSet<Website> Websites { get; set; }
    }
}

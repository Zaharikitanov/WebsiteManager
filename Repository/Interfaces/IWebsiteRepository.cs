using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteManager.Models.Data;

namespace WebsiteManager.Repository.Interfaces
{
    public interface IWebsiteRepository: IBaseRepository
    {
        Task<List<Website>> GetNotDeletedEntitiesAsync();
    }
}
using System.Net;
using WebsiteManager.Models;

namespace WebsiteManager.Factories.Interfaces
{
    public interface IStatusCodeResultFactory
    {
        HttpStatusCode Create(EntityActionOutcome entityOutcome);
    }
}
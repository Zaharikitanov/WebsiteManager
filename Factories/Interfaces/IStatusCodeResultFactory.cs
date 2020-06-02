using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebsiteManager.Models.Outcomes;

namespace WebsiteManager.Factories.Interfaces
{
    public interface IStatusCodeResultFactory
    {
        HttpStatusCode Create(CreateEntityOutcome createEntityOutcome);

        HttpStatusCode Update(UpdateEntityOutcome updateEntityOutcome);
    }
}
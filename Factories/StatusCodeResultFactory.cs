using System.Net;
using WebsiteManager.Factories.Interfaces;
using WebsiteManager.Models.Outcomes;

namespace WebsiteManager.Factories
{
    public class StatusCodeResultFactory : IStatusCodeResultFactory
    {
        public HttpStatusCode Create(CreateEntityOutcome createEntityOutcome)
        {
            switch (createEntityOutcome)
            {
                case CreateEntityOutcome.Success:
                    return HttpStatusCode.OK;

                case CreateEntityOutcome.CreateFailed:
                    return HttpStatusCode.Conflict;

                case CreateEntityOutcome.MissingFullEntityData:
                    return HttpStatusCode.UnprocessableEntity;

                default:
                    return HttpStatusCode.InternalServerError;
            }
        }

        public HttpStatusCode Update(UpdateEntityOutcome updateEntityOutcome)
        {
            switch (updateEntityOutcome)
            {
                case UpdateEntityOutcome.Success:
                    return HttpStatusCode.OK;

                case UpdateEntityOutcome.UpdateFailed:
                    return HttpStatusCode.UnprocessableEntity;

                case UpdateEntityOutcome.EntityNotFound:
                    return HttpStatusCode.Conflict;

                default:
                    return HttpStatusCode.InternalServerError;
            }
        }
    }
}

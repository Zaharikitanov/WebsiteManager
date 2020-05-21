using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebsiteManager.Factories.Interfaces;
using WebsiteManager.Models.Data;
using WebsiteManager.Models.Outcomes;
using WebsiteManager.Models.View;
using WebsiteManager.Repository.Interfaces;
using WebsiteManager.Services.Interfaces;

namespace WebsiteManager.Services
{
    public class WebsiteService : IWebsiteService
    {
        private IWebsiteRepository _repository;

        private IWebsiteFactory _factory;

        public WebsiteService(IWebsiteRepository repository, IWebsiteFactory factory)
        {
            _repository = repository;
            _factory = factory;
        }

        public async Task<CreateEntityOutcome> CreateEntityAsync(CreateNewWebsiteData viewData)
        {
            var newEntity = _factory.Create(viewData);

            var upsertSuccessful = await _repository.AddAsync(newEntity);
            if (upsertSuccessful == null)
            {
                return CreateEntityOutcome.CreateFailed;
            }

            return CreateEntityOutcome.Success;
        }

        public async Task<UpdateEntityOutcome> UpdateEntityAsync(WebsiteViewData viewData)
        {
            var getCurrent = await _repository.GetByIdAsync<Website>(viewData.Id);

            //var objectProperties = viewData.GetType().GetProperties();

            //bool isNull = objectProperties.Select(x => x.GetValue(viewData, null))
            //                    .Any(x => x == null);

            getCurrent.Name = viewData.Name ?? getCurrent.Name;
            getCurrent.URL = viewData.URL ?? getCurrent.URL;
            getCurrent.Category = viewData.Category ?? getCurrent.Category;
            getCurrent.HomepageSnapshot = viewData.HomepageSnapshot ?? getCurrent.HomepageSnapshot;
            getCurrent.Email = viewData.LoginDetails.Email ?? getCurrent.Email;
            getCurrent.Password = viewData.LoginDetails.Password ?? getCurrent.Password;
            getCurrent.EditedAt = DateTime.Now.ToString();

            var updateSuccessful = _repository.Update(getCurrent);
            if (updateSuccessful == null)
            {
                return UpdateEntityOutcome.UpdateFailed;
            }

            return UpdateEntityOutcome.Success;
        }

        public async Task<Website> GetEntityByIdAsync(Guid entityId)
        {
            return await _repository.GetByIdAsync<Website>(entityId);
        }

        public async Task<List<Website>> GetEntitiesListAsync()
        {
            return await _repository.GetNotDeletedEntitiesAsync();
        }

        public async Task<UpdateEntityOutcome> SoftDeleteEntityAsync(Guid entityId)
        {
            var getCurrent = await _repository.GetByIdAsync<Website>(entityId);

            getCurrent.IsDeleted = true;

            var updateSuccessful = _repository.Update(getCurrent);
            if (updateSuccessful == null)
            {
                return UpdateEntityOutcome.UpdateFailed;
            }

            return UpdateEntityOutcome.Success;
        }
    }
}
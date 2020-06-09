using EntityFrameworkPaginateCore;
using System;
using System.Threading.Tasks;
using WebsiteManager.Factories.Interfaces;
using WebsiteManager.Helpers;
using WebsiteManager.Models;
using WebsiteManager.Models.Database;
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

        public async Task<EntityActionOutcome> CreateEntityAsync(WebsiteInputData viewData)
        {
            var newEntity = _factory.Create(viewData);
            var validator = new WebsiteInputDataValidator();
            var result = validator.Validate(viewData);

            if (result.IsValid == false)
                return EntityActionOutcome.MissingFullEntityData;

            var upsertSuccessful = await _repository.AddAsync(newEntity);
            if (upsertSuccessful == null)
                return EntityActionOutcome.CreateFailed;

            return EntityActionOutcome.Success;
        }

        public async Task<EntityActionOutcome> UpdateEntityAsync(WebsiteInputData viewData, Guid id)
        {
            var getCurrent = await _repository.GetByIdAsync<Website>(id);
            var validator = new WebsiteInputDataValidator();
            var result = validator.Validate(viewData);

            if (result.IsValid == false)
                return EntityActionOutcome.UpdateFailed;

            var updateSuccessful = _repository.Update(await PopulateEntityDataWithViewData(viewData, id));
            if (updateSuccessful == null)
                return EntityActionOutcome.EntityNotFound;

            return EntityActionOutcome.Success;
        }

        public async Task<WebsiteViewData> GetEntityByIdAsync(Guid entityId)
        {
            return await _repository.GetByIdAsync(entityId);
        }

        public async Task<Page<WebsiteViewData>> GetPaginatedEntitiesAsync(int pageSize, int currentPage, string searchText, SortByOptions sortBy)
        {
            return await _repository.GetPaginatedResultsAsync(pageSize, currentPage, searchText, sortBy);
        }

        public async Task<EntityActionOutcome> SoftDeleteEntityAsync(Guid entityId)
        {
            var getCurrent = await _repository.GetByIdAsync<Website>(entityId);

            getCurrent.IsDeleted = true;

            var updateSuccessful = _repository.Update(getCurrent);
            if (updateSuccessful == null)
                return EntityActionOutcome.UpdateFailed;

            return EntityActionOutcome.Success;
        }

        private async Task<Website> PopulateEntityDataWithViewData(WebsiteInputData viewData, Guid entityId)
        {
            var getCurrent = await _repository.GetByIdAsync<Website>(entityId);

            getCurrent.Name = viewData.Name ?? getCurrent.Name;
            getCurrent.URL = viewData.URL ?? getCurrent.URL;
            getCurrent.Category = viewData.Category != getCurrent.Category ? viewData.Category : getCurrent.Category;
            getCurrent.HomepageSnapshot = viewData.HomepageSnapshot ?? getCurrent.HomepageSnapshot;
            getCurrent.Email = viewData.LoginDetails.Email ?? getCurrent.Email;
            getCurrent.Password = viewData.LoginDetails.Password ?? getCurrent.Password;
            getCurrent.EditedAt = DateTime.Now.ToString();

            return getCurrent;
        }
    }
}
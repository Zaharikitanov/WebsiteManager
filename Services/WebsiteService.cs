using EntityFrameworkPaginateCore;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;
using WebsiteManager.Factories.Interfaces;
using WebsiteManager.Helpers;
using WebsiteManager.Models;
using WebsiteManager.Models.Database;
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

        public async Task<CreateEntityOutcome> CreateEntityAsync(WebsiteInputData viewData)
        {
            var newEntity = _factory.Create(viewData);

            WebsiteInputDataValidator validator = new WebsiteInputDataValidator();

            ValidationResult result = validator.Validate(viewData);

            if (result.IsValid == false)
            {
                return CreateEntityOutcome.MissingFullEntityData;
            }

            var upsertSuccessful = await _repository.AddAsync(newEntity);
            if (upsertSuccessful == null)
            {
                return CreateEntityOutcome.CreateFailed;
            }

            return CreateEntityOutcome.Success;
        }

        public async Task<UpdateEntityOutcome> UpdateEntityAsync(WebsiteInputData viewData, Guid id)
        {
            var getCurrent = await _repository.GetByIdAsync<Website>(id);
            
            getCurrent.Name = viewData.Name ?? getCurrent.Name;
            getCurrent.URL = viewData.URL ?? getCurrent.URL;
            getCurrent.Category = viewData.Category != getCurrent.Category ? viewData.Category : getCurrent.Category;
            getCurrent.HomepageSnapshot = viewData.HomepageSnapshot ?? getCurrent.HomepageSnapshot;
            getCurrent.Email = viewData.LoginDetails.Email ?? getCurrent.Email;
            getCurrent.Password = viewData.LoginDetails.Password ?? getCurrent.Password;
            getCurrent.EditedAt = DateTime.Now.ToString();

            WebsiteInputDataValidator validator = new WebsiteInputDataValidator();

            ValidationResult result = validator.Validate(viewData);

            if (result.IsValid == false)
            {
                return UpdateEntityOutcome.UpdateFailed;
            }

            var updateSuccessful = _repository.Update(getCurrent);
            if (updateSuccessful == null)
            {
                return UpdateEntityOutcome.EntityNotFound;
            }

            return UpdateEntityOutcome.Success;
        }

        public async Task<WebsiteViewData> GetEntityByIdAsync(Guid entityId)
        {
            return await _repository.GetByIdAsync(entityId);
        }

        public async Task<Page<WebsiteViewData>> GetPaginatedEntitiesAsync(int pageSize, int currentPage, string searchText, SortByOptions sortBy)
        {
            return await _repository.GetPaginatedResultsAsync(pageSize, currentPage, searchText, sortBy);
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
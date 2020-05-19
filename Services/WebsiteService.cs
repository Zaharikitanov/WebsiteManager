using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteManager.Factories.Interfaces;
using WebsiteManager.Models.Data;
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

        public async void CreateEntityAsync(WebsiteViewData viewData)
        {
            var newEntity = _factory.Create(viewData);

            await _repository.AddAsync(newEntity);
        }

        public async void UpdateEntityAsync(WebsiteViewData viewData)
        {
            var getCurrent = await _repository.GetByIdAsync<Website>(viewData.Id);

            getCurrent.Name = viewData.Name ?? getCurrent.Name;
            getCurrent.URL = viewData.URL ?? getCurrent.URL;
            getCurrent.Category = viewData.Category ?? getCurrent.Category;
            getCurrent.HomepageSnapshot = viewData.HomepageSnapshot ?? getCurrent.HomepageSnapshot;
            getCurrent.Email = viewData.LoginDetails.Email ?? getCurrent.Email;
            getCurrent.Password = viewData.LoginDetails.Password ?? getCurrent.Password;
            getCurrent.EditedAt = DateTime.Now.ToString();

            await _repository.Update(getCurrent);
        }

        public async Task<Website> GetEntityByIdAsync(Guid entityId)
        {
            return await _repository.GetByIdAsync<Website>(entityId);
        }

        public async Task<List<Website>> GetEntitiesListAsync()
        {
            return await _repository.ListAsync<Website>();
        }

        public async void SoftDeleteEntityAsync(Guid entityId)
        {
            var getCurrent = await _repository.GetByIdAsync<Website>(entityId);

            getCurrent.IsDeleted = true;

            await _repository.Update(getCurrent);
        }
    }
}
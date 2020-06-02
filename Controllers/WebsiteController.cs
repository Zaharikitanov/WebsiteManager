using EntityFrameworkPaginateCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using WebsiteManager.Factories.Interfaces;
using WebsiteManager.Models;
using WebsiteManager.Models.View;
using WebsiteManager.Services.Interfaces;

namespace WebsiteManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebsiteController : ControllerBase
    {
        private IWebsiteService _service;
        private IStatusCodeResultFactory _resultFactory;

        public WebsiteController(IWebsiteService service, IStatusCodeResultFactory factory)
        {
            _service = service;
            _resultFactory = factory;
        }

        [HttpPost]
        public async Task<HttpStatusCode> Create(WebsiteInputData inputData)
        {
            var createEntityOutcome = await _service.CreateEntityAsync(inputData);

            return _resultFactory.Create(createEntityOutcome);
        }
        
        [HttpGet]
        public async Task<Page<WebsiteViewData>> GetAllEntities(
            int pageSize = 10, 
            int currentPage = 1, 
            string searchText = "", 
            SortByOptions sortBy = SortByOptions.Name
            )
        {
            return await _service.GetPaginatedEntitiesAsync(pageSize, currentPage, searchText, sortBy);
        }

        [HttpGet("{id}")]
        public async Task<WebsiteViewData> GetEntityById(Guid id)
        {
            return await _service.GetEntityByIdAsync(id);
        }

        [HttpPut("{id}")]
        public async Task<HttpStatusCode> Update(WebsiteInputData inputData, Guid id)
        {
            var updateEntityOutcome = await _service.UpdateEntityAsync(inputData, id);

            return _resultFactory.Update(updateEntityOutcome);
        }

        [HttpPut("{id}/softdelete")]
        public async Task<HttpStatusCode> Delete(Guid id)
        {
            var updateEntityOutcome = await _service.SoftDeleteEntityAsync(id);

            return _resultFactory.Update(updateEntityOutcome);
        }
    }
}
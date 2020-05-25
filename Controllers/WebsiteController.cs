using EntityFrameworkPaginateCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using WebsiteManager.Models;
using WebsiteManager.Models.Outcomes;
using WebsiteManager.Models.View;
using WebsiteManager.Services.Interfaces;

namespace WebsiteManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebsiteController : ControllerBase
    {
        private IWebsiteService _service;

        public WebsiteController(IWebsiteService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<StatusCodeResult> Create(WebsiteInputData inputData)
        {
            var createEntityOutcome = await _service.CreateEntityAsync(inputData);

            switch (createEntityOutcome)
            {
                case CreateEntityOutcome.Success:
                    return Ok();

                case CreateEntityOutcome.CreateFailed:
                    return Conflict();

                case CreateEntityOutcome.MissingFullEntityData:
                    return UnprocessableEntity();

                default:
                    return StatusCode((int)HttpStatusCode.InternalServerError);
            }
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
        public async Task<StatusCodeResult> Update(WebsiteInputData inputData, Guid id)
        {
            var updateEntityOutcome = await _service.UpdateEntityAsync(inputData, id);

            switch (updateEntityOutcome)
            {
                case UpdateEntityOutcome.Success:
                    return Ok();

                case UpdateEntityOutcome.UpdateFailed:
                    return UnprocessableEntity();

                case UpdateEntityOutcome.EntityNotFound:
                    return Conflict();

                default:
                    return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut("{id}/softdelete")]
        public async Task<StatusCodeResult> Delete(Guid id)
        {
            var updateEntityOutcome = await _service.SoftDeleteEntityAsync(id);

            switch (updateEntityOutcome)
            {
                case UpdateEntityOutcome.Success:
                    return Ok();

                case UpdateEntityOutcome.UpdateFailed:
                    return UnprocessableEntity();

                default:
                    return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
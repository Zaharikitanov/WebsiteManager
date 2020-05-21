using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WebsiteManager.Models.Data;
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
        public void Create(WebsiteViewData inputData)
        {
            _service.CreateEntityAsync(inputData);
        }

        [HttpGet]
        public async Task<List<Website>> GetAllEntities()
        {
            return await _service.GetEntitiesListAsync(); 
        }

        [HttpGet("{id}")]
        public async Task<Website> GetEntityById(Guid id)
        {
            return await _service.GetEntityByIdAsync(id);
        }

        [HttpPut]
        public async Task<IActionResult> Update(WebsiteViewData inputData)
        {
            var updateEntityOutcome = await _service.UpdateEntityAsync(inputData);

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

        [HttpPut("{id}/softdelete")]
        public async Task<IActionResult> Delete(Guid id)
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
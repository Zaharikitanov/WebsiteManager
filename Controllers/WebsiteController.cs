using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteManager.Models.Data;
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
        public void Create([FromQuery]WebsiteViewData inputData)
        {
            _service.CreateEntityAsync(inputData);
        }

        [HttpGet]
        public async Task<List<Website>> GetAllEntities()
        {
            return await _service.GetEntitiesListAsync(); 
        }

        [HttpGet("{id}")]
        public async Task<Website> GetEntityById([FromQuery]Guid id)
        {
            return await _service.GetEntityByIdAsync(id);
        }

        [HttpPut]
        public void Update([FromQuery]WebsiteViewData inputData)
        {
            _service.UpdateEntityAsync(inputData);
        }

        [HttpPut("{id}/softdelete")]
        public void Delete([FromQuery]Guid id)
        {
            _service.SoftDeleteEntityAsync(id);
        }
    }
}
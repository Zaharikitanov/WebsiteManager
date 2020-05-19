using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public void Update(WebsiteViewData inputData)
        {
            _service.UpdateEntityAsync(inputData);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _service.SoftDeleteEntityAsync(id);
        }
    }
}
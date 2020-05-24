﻿using EntityFrameworkPaginateCore;
using System;
using System.Threading.Tasks;
using WebsiteManager.Models;
using WebsiteManager.Models.Outcomes;
using WebsiteManager.Models.View;

namespace WebsiteManager.Services.Interfaces
{
    public interface IWebsiteService
    {
        Task<CreateEntityOutcome> CreateEntityAsync(CreateNewWebsiteData viewData);

        Task<WebsiteViewData> GetEntityByIdAsync(Guid entityId);

        Task<UpdateEntityOutcome> SoftDeleteEntityAsync(Guid entityId);

        Task<UpdateEntityOutcome> UpdateEntityAsync(WebsiteViewData viewData);

        Task<Page<WebsiteViewData>> GetPaginatedEntitiesAsync(int pageSize, int currentPage, string searchText, SortByOptions sortBy);
    }
}
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteManager.DatabaseContext;
using WebsiteManager.Models.Database;
using WebsiteManager.Repository.Interfaces;

namespace WebsiteManager.Repository
{
    public abstract class BaseRepository : IBaseRepository
    {
        public WebsiteManagerContext _dbContext;

        public BaseRepository(WebsiteManagerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> GetByIdAsync<T>(Guid id) where T : Entity
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
        }

        public virtual async Task<List<T>> ListAsync<T>() where T : Entity
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<T> AddAsync<T>(T entity) where T : Entity
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<T> DeleteAsync<T>(T entity) where T : Entity
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public T Update<T>(T entity) where T : Entity
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return entity;
        }
    }
}
using E_Market.Core.Application.Interfaces;
using E_Market.Core.Application.Interfaces.Repository;
using E_Market.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
    {
        public readonly ApplicationContext _dbContext;

        public GenericRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<Entity> AddAsync(Entity entity)
        {
            await _dbContext.Set<Entity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task EditAsync(Entity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(Entity entity)
        {
            _dbContext.Set<Entity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<List<Entity>> GetAllAsync()
        {
            return await _dbContext.Set<Entity>().ToListAsync();
        }

        public virtual async Task<List<Entity>> GetAllWithIncludeAsync(List<String> properties)
        {
            var query = _dbContext.Set<Entity>().AsQueryable();

            foreach (string property in properties)
            {
                query = query.Include(property);
            }

            return await query.ToListAsync();
        }

        public virtual async Task<Entity> GetAllWithIncludeAsync(int id, List<String> properties)
        {
            var query = await _dbContext.Set<Entity>().FindAsync(id);

            foreach (string property in properties)
            {
                _dbContext.Entry(query).Reference(property).Load();
            }

            return query;
        }

        public virtual async Task<Entity> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Entity>().FindAsync(id);
        }
    }
}

//        public async Task AddAsync(Entity entity)
//        {
//            await _dbContext.Set<Entity>().AddAsync(entity);
//            await _dbContext.SaveChangesAsync();

//        }

//        public async Task EditAsync(Entity entity)
//        {
//            _dbContext.Entry(entity).State = EntityState.Modified;
//            await _dbContext.SaveChangesAsync();
//        }

//        public async Task DeleteAsync(Entity entity)
//        {
//            _dbContext.Set<Entity>().Remove(entity);
//            await _dbContext.SaveChangesAsync();
//        }

//        public async Task<List<Entity>> GetAllAsync()
//        {
//            return await _dbContext.Set<Entity>().ToListAsync();
//        }

//        public async Task<List<Entity>> GetAllWithIncludeAsync(List<String> properties)
//        {
//            var query = _dbContext.Set<Entity>().AsQueryable();

//            foreach (string property in properties)
//            {
//                query = query.Include(property);
//            }

//            return await query.ToListAsync();
//        }

//        public async Task<Entity> GetByIdAsync(int id)
//        {
//            return await _dbContext.Set<Entity>().FindAsync(id);
//        }

// 

//    }
//}

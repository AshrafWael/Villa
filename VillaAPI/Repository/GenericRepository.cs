﻿using Microsoft.EntityFrameworkCore;
using VillaAPI.Data;
using VillaAPI.IRepository;
using System.Linq.Expressions;
namespace VillaAPI.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        internal DbSet<T> _dbset;
        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbset = _dbContext.Set<T>();   
        }
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T,bool>> filter = null,
            string?[] includs = null, int pagesize = 0,int pagenumber=1)
        {
            IQueryable<T> query = _dbset;
            if (filter != null) 
            {
              query = query.Where(filter);
            }
            if(includs !=null) 
            {
                foreach (var item in includs)
                {
                    query = query.Include(item);
                }
            }
            if (pagesize >0)
            {
                if (pagesize > 100) 
                { pagesize = 100; }
                query = query.Skip(pagesize*(pagenumber-1)).Take(pagesize); 
            }
            return await query.ToListAsync();
        }
        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null,
            string?[] includs = null, bool tracked = true)
        {
            IQueryable<T> query = _dbset;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includs != null)
            {
                foreach (var item in includs)
                {
                    query = query.Include(item);
                }
            }
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync();
        }
        public async Task CreateAsync(T entity)
        {
            await _dbset.AddAsync(entity);
           await SaveAsync();
        }
        public async Task RemoveAsync(T entity)
        {
            _dbset.Remove(entity);
            await SaveAsync();
        }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}

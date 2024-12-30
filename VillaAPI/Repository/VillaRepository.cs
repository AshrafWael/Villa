using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using VillaAPI.Data;
using VillaAPI.Dtos;
using VillaAPI.IRepository;
using VillaAPI.Models;

namespace VillaAPI.Repository
{
    public class VillaRepository : GenericRepository<Villa> ,IVillaRepository 
    {
        private readonly ApplicationDbContext _dbContext;

        public VillaRepository(ApplicationDbContext dbContext) :base(dbContext) 
        { 
            _dbContext = dbContext;
        }
        public async Task<Villa> UpdateAsync(Villa entity)
        {
            entity.UpdatededDtae = DateTime.Now;
            _dbContext.Villas.Update(entity);
           await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}

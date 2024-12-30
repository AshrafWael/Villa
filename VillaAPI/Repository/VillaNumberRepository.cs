using VillaAPI.Data;
using VillaAPI.IRepository;
using VillaAPI.Models;

namespace VillaAPI.Repository
{
    public class VillaNumberRepository : GenericRepository<VillaNumber>, IVillaNumberRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public VillaNumberRepository(ApplicationDbContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task<VillaNumber> UpdateAsync(VillaNumber entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _dbContext.villaNumbers.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}

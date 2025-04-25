using VillaAPI.Models;

namespace VillaAPI.IRepository
{
    public interface IVillaNumberRepository :IGenericRepository<VillaNumber>
    {
        Task<VillaNumber> UpdateAsync(VillaNumber entity, string[] includs = null);

    }
}

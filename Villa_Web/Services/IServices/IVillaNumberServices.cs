using System.Linq.Expressions;
using Villa_Web.Dtos.VillaDtos;
using Villa_Web.Dtos.VillaNumberDtos;

namespace Villa_Web.Services.IServices
{
    public interface IVillaNumberServices
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>( AddVillaNumberDto villaDto);
        Task<T> UpdateAsync<T>(UpdateVillaNumberDto updateVillaDto );
        Task<T> DeleteAsync<T>(int id);

    }
}

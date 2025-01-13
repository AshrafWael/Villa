using System.Linq.Expressions;
using Villa_Web.Dtos.VillaDtos;

namespace Villa_Web.Services.IServices
{
    public interface IVillaServices
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task CreateAsync<T>( AddVillaDto villaDto);
        Task UpdateAsync<T>(UpdateVillaDto updateVillaDto );
        Task DeleteAsync<T>(int id);

    }
}

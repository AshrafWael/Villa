using System.Linq.Expressions;
using VillaAPI.Dtos;
using VillaAPI.Models;

namespace VillaAPI.IRepository
{
    public interface IVillaRepository : IGenericRepository<Villa> 
    {
        Task<Villa> UpdateAsync(Villa entity);
    }
}

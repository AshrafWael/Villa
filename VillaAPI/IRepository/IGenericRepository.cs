using System.Linq.Expressions;
using VillaAPI.Dtos;
using VillaAPI.Models;

namespace VillaAPI.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string?[] includs = null,
            int pagesize= 0,int pagenumber = 1);
        Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, string?[] includs = null, bool tracked = true);
        Task CreateAsync(T entity);
        Task RemoveAsync(T entity);
        Task SaveAsync();
    }
}

using Villa_Web.Models;
using Villa_Web.Responses;

namespace Villa_Web.Services.IServices
{
    public interface IBaseSrvice
    {
        APIResponse ResponceModel { get; set; }
        Task<T> SendAsync<T>(APIRequest apirequest);  
          
    }
}

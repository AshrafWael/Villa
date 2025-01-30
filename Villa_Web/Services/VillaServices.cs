using Villa_Utility;
using Villa_Web.Dtos.VillaDtos;
using Villa_Web.Models;
using Villa_Web.Services.IServices;

namespace Villa_Web.Services
{
    public class VillaServices : BaseService, IVillaServices
    {
        private readonly IHttpClientFactory _httpClient;
        private string VillaUrl;
        public VillaServices(IHttpClientFactory httpClient,IConfiguration configuration) : base(httpClient) 
        {
            _httpClient = httpClient;
            VillaUrl = configuration.GetValue<string>("ServiseUrls:VillAPI")!;
        }
        public Task<T> CreateAsync<T>(AddVillaDto villaDto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDta.ApiType.POST,
                Data = villaDto,
                ApiUrl = VillaUrl + "/api/v1/VillaAPI",
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDta.ApiType.DELETE,
                ApiUrl = VillaUrl + "/api/v1/VillaAPI/"+id,
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDta.ApiType.GET,
                ApiUrl = VillaUrl + "/api/v1/VillaAPI",
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDta.ApiType.GET,
                ApiUrl = VillaUrl + "/api/v1/VillaAPI/"+id,
            });
        }

        public Task<T> UpdateAsync<T>(UpdateVillaDto updateVillaDto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDta.ApiType.PUT,
                Data = updateVillaDto,
                ApiUrl = VillaUrl + "/api/v1/VillaAPI/"+updateVillaDto.Id,
            });
        }
    }
}

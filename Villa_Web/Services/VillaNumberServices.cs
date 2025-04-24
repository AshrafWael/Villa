using Villa_Utility;
using Villa_Web.Dtos.VillaDtos;
using Villa_Web.Dtos.VillaNumberDtos;
using Villa_Web.Models;
using Villa_Web.Services.IServices;

namespace Villa_Web.Services
{
    public class VillaNumberServices : BaseService, IVillaNumberServices
    {
        private readonly IHttpClientFactory _httpClient;
        private string VillaUrl;
        public VillaNumberServices(IHttpClientFactory httpClient,IConfiguration configuration) : base(httpClient) 
        {
            _httpClient = httpClient;
            VillaUrl = configuration.GetValue<string>("ServiseUrls:VillAPI")!;
        }
        public Task<T> CreateAsync<T>(AddVillaNumberDto villaDto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDta.ApiType.POST,
                Data = villaDto,
                ApiUrl = VillaUrl + "/api/v1/VillaNumberAPI",
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDta.ApiType.DELETE,
                ApiUrl = VillaUrl + "/api/v1/VillaNumberAPI/" + id,
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDta.ApiType.GET,
                ApiUrl = VillaUrl + "/api/v1/VillaNumberAPI",
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDta.ApiType.GET,
                ApiUrl = VillaUrl + "/api/v1/VillaNumberAPI/" + id,
            });
        }

        public Task<T> UpdateAsync<T>(UpdateVillaNumberDto updateVillaDto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDta.ApiType.PUT,
                Data = updateVillaDto,
                ApiUrl = VillaUrl + "/api/v1/VillaNumberAPI/" + updateVillaDto.VillaNo,
            });
        }
    }
}

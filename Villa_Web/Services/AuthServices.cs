using Villa_Utility;
using Villa_Web.Dtos.AccountUserDtos;
using Villa_Web.Models;
using Villa_Web.Services.IServices;

namespace Villa_Web.Services
{
    public class AuthServices : BaseService, IAuthServices
    {
        private readonly IHttpClientFactory _httpClient;
        private string VillaUrl;
        public AuthServices(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            _httpClient = httpClient;
            VillaUrl = configuration.GetValue<string>("ServiseUrls:VillAPI")!;
        }
        public Task<T> LoginAsync<T>(LoginRequestDto logindto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDta.ApiType.POST,
                Data = logindto,
                ApiUrl = VillaUrl + "/api/v1/Users/Login",
            });
        }

        public Task<T> RegisterAsync<T>(RegisterRequestDto registerdto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDta.ApiType.POST,
                Data = registerdto,
                ApiUrl = VillaUrl + "/api/v1/Users/Register",
            });
        }
    }
}

using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Villa_Utility;
using Villa_Web.Models;
using Villa_Web.Responses;
using Villa_Web.Services.IServices;

namespace Villa_Web.Services
{
    public class BaseService : IBaseSrvice
    {
        //To call our API 
        public IHttpClientFactory _httpClient {  get; set; }
        public BaseService(IHttpClientFactory httpClient)
        {
            this.ResponceModel = new();
            _httpClient = httpClient;
        }
        public APIResponse ResponceModel 
        {
            get ;
            set; 

        }

        public async Task<T> SendAsync<T>(APIRequest apirequest)
        {
            try
            {
                var Client = _httpClient.CreateClient("Villa API");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apirequest.ApiUrl);
                if (apirequest.Data != null) 
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apirequest.Data)
                        , Encoding.UTF8, "application/json");
                }
                switch (apirequest.ApiType) 
                {
                    case StaticDta.ApiType.POST :
                        message.Method = HttpMethod.Post;
                        break;
                    case StaticDta.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case StaticDta.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }
                HttpResponseMessage apiResponse = null;
                apiResponse = await Client.SendAsync(message);
                var apicontent = await apiResponse.Content.ReadAsStringAsync();
                var ApiResponse = JsonConvert.DeserializeObject<T>(apicontent);
                return ApiResponse;

            }
            catch (Exception ex)
            {
                var dto = new APIResponse
                {
                    Errors = new List<string>() { Convert.ToString(ex.Message) },
                    IsSuccess = false,
                };
                var result = JsonConvert.SerializeObject(dto);
                var ApiResponse = JsonConvert.DeserializeObject<T>(result);
                return ApiResponse;

            }
        }
}

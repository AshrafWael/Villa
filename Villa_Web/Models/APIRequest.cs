using Microsoft.AspNetCore.Mvc;
using static Villa_Utility.StaticDta;

namespace Villa_Web.Models
{
    public class APIRequest
    {

        public ApiType ApiType { get; set; } = ApiType.GET;
        public string ApiUrl { get; set; }
        public object Data { get; set; }
    }
}

using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Villa_Web.Dtos.VillaDtos;
using Villa_Web.Models;
using Villa_Web.Responses;
using Villa_Web.Services.IServices;

namespace Villa_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVillaServices _villaServices;
        private readonly IMapper _Mapper;

        public HomeController(IVillaServices villaServices, IMapper mapper)
        {
            _villaServices = villaServices;
            _Mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            List<ReadVillaDto> list = new();
            var response = await _villaServices.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ReadVillaDto>>(Convert.ToString(response.Result)!)!;
            }
            return View(list);
        }

     

        public IActionResult Privacy()
        {
            return View();
        }

    }
}

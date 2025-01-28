using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Villa_Web.Dtos.VillaDtos;
using Villa_Web.Responses;
using Villa_Web.Services.IServices;

namespace Villa_Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaServices _villaServices;
        private readonly IMapper _Mapper;

        public VillaController(IVillaServices villaServices,IMapper mapper)
        {
            _villaServices = villaServices;
            _Mapper = mapper;
        }

        public async Task<IActionResult> IndexVilla()
        {
            List<ReadVillaDto> list = new();
            var response = await _villaServices.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess) 
            {
            list = JsonConvert.DeserializeObject<List<ReadVillaDto>>(Convert.ToString(response.Result)!)!;
            }
            return View(list);
        }
    }
}

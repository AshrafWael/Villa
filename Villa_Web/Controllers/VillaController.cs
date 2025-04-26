using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Villa_Web.Dtos.VillaDtos;
using Villa_Web.Models;
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
        public async Task<IActionResult> CreateVilla()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVilla(AddVillaDto villadto)
        {
            if (ModelState.IsValid) 
            {
            var villa = await _villaServices.CreateAsync<APIResponse>(villadto);
                if (villa != null && villa.IsSuccess) 
                {
                    return RedirectToAction(nameof(IndexVilla));
                }
             }
            return View(villadto);
        }
       
        public async Task<IActionResult> UpdateVilla(int id)
        {
            var villa = await _villaServices.GetAsync<APIResponse>(id);
            if (villa != null && villa.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<Villa>(Convert.ToString(villa.Result)!)!;
                var mappedvilla = _Mapper.Map<UpdateVillaDto>(model);
                return View(mappedvilla);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVilla(UpdateVillaDto villaDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaServices.UpdateAsync<APIResponse>(villaDto);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVilla));
                }
            }
            return View(villaDto);
        }

        public async Task<IActionResult> DeleteVilla(int id)
        {
            var villa = await _villaServices.GetAsync<APIResponse>(id);
            if (villa != null && villa.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<Villa>(Convert.ToString(villa.Result)!)!;
                var mappedvilla = _Mapper.Map<ReadVillaDto>(model);
                return View(mappedvilla);
            }
        
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVilla(ReadVillaDto villaDto)
        {
           
                var response = await _villaServices.DeleteAsync<APIResponse>(villaDto.Id);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVilla));
                }
            
            return View(villaDto);
        }
    }
}

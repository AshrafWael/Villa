using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Villa_Web.Dtos.VillaDtos;
using Villa_Web.Dtos.VillaNumberDtos;
using Villa_Web.Models;
using Villa_Web.Responses;
using Villa_Web.Services.IServices;
using Villa_Web.ViewModels;

namespace Villa_Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberServices _villaNumberServices;
        private readonly IMapper _Mapper;
        private readonly IVillaServices _villaServices;

        public VillaNumberController(IVillaNumberServices villaNumberServices,IMapper mapper,IVillaServices villaServices)
        {
            _villaNumberServices = villaNumberServices;
            _Mapper = mapper;
            _villaServices = villaServices;
        }
        public async Task<IActionResult> IndexVillaNumber()
        {
            List<ReadVillaNumberDto> list = new();
            var response = await _villaNumberServices.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ReadVillaNumberDto>>(Convert.ToString(response.Result)!)!;
            }
            return View(list);
         }
        public async Task<IActionResult> CreateVillaNumber()
        {
           CreateVillaNumberVM villanumbervm = new();
            var response = await _villaServices.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                villanumbervm.VillaList = JsonConvert.DeserializeObject<List<ReadVillaDto>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                          Text = i.Name,
                          Value = i.Id.ToString()
                    });
            }
            return View(villanumbervm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVillaNumber(CreateVillaNumberVM villadto)
        {
            if (ModelState.IsValid) 
            {
                var villa = await _villaNumberServices.CreateAsync<APIResponse>(villadto.VillaNumber);
                if (villa != null && villa.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
                else
                {
                    if (villa.Errors.Count>0) 
                    {
                        ModelState.AddModelError("Errors", villa.Errors.FirstOrDefault());    
                    }
                }
             }

            var response = await _villaServices.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                villadto.VillaList = JsonConvert.DeserializeObject<List<ReadVillaDto>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
            }
            return View(villadto);

        }
       
        public async Task<IActionResult> UpdateVillaNumber(int villano)
        {
            UpdateVillaNumberVM villanumbervm = new();
            var villa = await _villaNumberServices.GetAsync<APIResponse>(villano);
            if (villa != null && villa.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<VillaNumber>(Convert.ToString(villa.Result)!)!;
               villanumbervm.VillaNumber = _Mapper.Map<UpdateVillaNumberDto>(model);
            }
            var response = await _villaServices.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                villanumbervm.VillaList = JsonConvert.DeserializeObject<List<ReadVillaDto>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
                return View(villanumbervm);
            }
                return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVillaNumber(UpdateVillaNumberVM villaDto)
        {
            if (ModelState.IsValid)
            {
                var villa = await _villaNumberServices.UpdateAsync<APIResponse>(villaDto.VillaNumber);
                if (villa != null && villa.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
                else
                {
                    if (villa.Errors.Count > 0)
                    {
                        ModelState.AddModelError("Errors", villa.Errors.FirstOrDefault());
                    }
                }
            }

            var response = await _villaServices.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                villaDto.VillaList = JsonConvert.DeserializeObject<List<ReadVillaDto>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
            }
            return View(villaDto);
        }

        public async Task<IActionResult> DeleteVillaNumber(int villano)
        {
            ReadVillaNumberVM villanumbervm = new();
            var villa = await _villaNumberServices.GetAsync<APIResponse>(villano);
            if (villa != null && villa.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<VillaNumber>(Convert.ToString(villa.Result)!)!;
                villanumbervm.VillaNumber = _Mapper.Map<ReadVillaNumberDto>(model);
            }
            var response = await _villaServices.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                villanumbervm.VillaList = JsonConvert.DeserializeObject<List<ReadVillaDto>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
                return View(villanumbervm);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVillaNumber(ReadVillaNumberVM villaDto)
        {
           
                var response = await _villaNumberServices.DeleteAsync<APIResponse>(villaDto.VillaNumber.VillaNo);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
            
            return View(villaDto);
        }
    }
}

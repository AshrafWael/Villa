using System.Security.Principal;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using VillaAPI.Data;
using VillaAPI.IRepository;
using VillaAPI.Models;
using VillaAPI.Responses;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics.Eventing.Reader;
using VillaAPI.Dtos.VillaDtos;

namespace VillaAPI.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]

    public class VillaAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IVillaRepository _villarepo;

        public VillaAPIController(ApplicationDbContext dbContext, IMapper mapper, IVillaRepository villarepo)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _villarepo = villarepo;
            _response = new();
        }
        [HttpGet]
        //Documnt Response 
        [ProducesResponseType(200)]
        //  [Authorize]
        [ResponseCache(Duration = 30)]
        public async Task<ActionResult<APIResponse>> GetVillas([FromQuery(Name = "FilterOcupancy")] int? Ocupancy,
            [FromQuery(Name = "Serch Filter")] string? Search)
        {
            try
            {
                IEnumerable<Villa> VillaList;

                if (Ocupancy >0) 
                {
                    VillaList=  await _villarepo.GetAllAsync(u=> u.Occupancy == Ocupancy);
                }
                else
                {
                    VillaList = await _villarepo.GetAllAsync();
                }
                if(!string.IsNullOrEmpty(Search))
                {
                    VillaList = await _villarepo.GetAllAsync(u => u.Name.ToLower().Contains(Search));
                }
                var mappedvilla = _mapper.Map<List<ReadVillaDto>>(VillaList);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = mappedvilla;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.Errors = new List<string> { ex.Message };
            }
            return _response;
        }
        [ResponseCache(CacheProfileName = "Default30")]
        [HttpGet("id", Name = "GetVilla")]
        //  [Authorize(Roles ="user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> GetVillaById(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess= false;
                    return BadRequest(_response);
                }
                var villa = await _villarepo.GetAsync(a => a.Id == id);
                if (villa == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }
                var mappedvilla = _mapper.Map<ReadVillaDto>(villa);
                _response.IsSuccess = true;
                _response.Result = mappedvilla;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Errors = new List<string> { ex.Message };
            }
            return _response;
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> AddVilla([FromBody] AddVillaDto createdvilla)
        {
            try
            {
                if (await _villarepo.GetAsync(u => u.Name.ToLower() == createdvilla.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("createdError", "Villa name  is already Exists");
                    return BadRequest(ModelState);
                }
                if (createdvilla == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.Result = createdvilla;
                    return BadRequest(_response);
                }
                var villamodel = _mapper.Map<Villa>(createdvilla);
                await _villarepo.CreateAsync(villamodel);

                _response.IsSuccess = true;
                _response.Result = villamodel;
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetVilla", new { id = villamodel.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Errors = new List<string> { ex.Message };
            }
            return _response;

        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("id", Name = "DeleteVilla")]
        [Authorize(Roles = "Custom")]
        public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.Result = null;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                var villa = await _villarepo.GetAsync(a => a.Id == id);
                if (villa == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }
                await _villarepo.RemoveAsync(villa);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Errors = new List<string> { ex.Message };
            }
            return _response;
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("id", Name = "UpdateVilla")]
        public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody] UpdateVillaDto villadto)
        {
            try
            {
                if (villadto == null || id != villadto.Id)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;

                    return BadRequest(_response);
                }
                var villamodel = _mapper.Map<Villa>(villadto);
                await _villarepo.UpdateAsync(villamodel);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Errors = new List<string> { ex.Message };
            }
            return _response;
        }
        [HttpPatch("{id:int}", Name = "UpdatePertialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePertialVilla(int id, JsonPatchDocument<UpdateVillaDto> PatchDto)
        {
            if (PatchDto == null || id == 0)
            {
                return BadRequest();
            }
            var villa = await _villarepo.GetAsync(a => a.Id == id);
            var villadto = _mapper.Map<UpdateVillaDto>(villa);
            if (villa == null)
            {
                return BadRequest();
            }
            PatchDto.ApplyTo(villadto);
            var villamodel = _mapper.Map<Villa>(villadto);
            await _villarepo.UpdateAsync(villamodel);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return NoContent();


        }


    }
}

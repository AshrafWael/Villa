using System.Security.Principal;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using VillaAPI.Data;
using VillaAPI.Dtos;
using VillaAPI.IRepository;
using VillaAPI.Models;
using VillaAPI.Responses;
using VillaAPI.Dtos.VillaNumberDtos;

namespace VillaAPI.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]

    public class VillaNumberAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IVillaNumberRepository _villanumberrepo;
        private readonly IVillaRepository _villaRepository;

        public VillaNumberAPIController(ApplicationDbContext dbContext, IMapper mapper,
                        IVillaNumberRepository villanumberrepo, IVillaRepository villaRepository)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _villanumberrepo = villanumberrepo;
            _villaRepository = villaRepository;
            _response = new();
        }
        [HttpGet]
        //Documnt Response 
        [ProducesResponseType(200)]
        public async Task<ActionResult<APIResponse>> GetVillasNumbers()
        {
            try
            {
                IEnumerable<VillaNumber> VillanumberList = await _villanumberrepo.GetAllAsync();
                var mappedvilla = _mapper.Map<List<ReadVillaNumberDto>>(VillanumberList);

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
        [HttpGet("id", Name = "GetVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVillaNumber(int number)
        {
            try
            {
                if (number == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var villa = await _villanumberrepo.GetAsync(a => a.VillaNo == number);
                if (villa == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                var mappedvilla = _mapper.Map<ReadVillaNumberDto>(villa);
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody] AddVillaNumberDto createdvilla)
        {
            try
            {
                if (await _villanumberrepo.GetAsync(u => u.VillaNo == createdvilla.VillaNo) != null)
                {
                    ModelState.AddModelError("createdError", "Villa Number is already Exists");
                    return BadRequest(ModelState);
                }
                if (await _villaRepository.GetAsync(u => u.Id == createdvilla.VillaId) == null)
                {
                    ModelState.AddModelError("createdError", "Villa ID is Invalid!");
                    return BadRequest(ModelState);
                }
                if (createdvilla == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.Result = createdvilla;
                    return BadRequest(_response);
                }
                var villamodel = _mapper.Map<VillaNumber>(createdvilla);
                await _villanumberrepo.CreateAsync(villamodel);

                _response.IsSuccess = true;
                _response.Result = villamodel;
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetVilla", new { number = villamodel.VillaNo }, _response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.Errors = new List<string> { ex.Message };
            }
            return _response;

        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{number:int}", Name = "DeleteVillaNumber")]
        public async Task<ActionResult<APIResponse>> DeleteVillaNumber(int number)
        {
            try
            {
                if (number == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.Result = null;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                var villa = await _villanumberrepo.GetAsync(a => a.VillaNo == number);
                if (villa == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }
                await _villanumberrepo.RemoveAsync(villa);
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
        [HttpPut("{number:int}", Name = "UpdateVillaNumber")]
        public async Task<ActionResult<APIResponse>> UpdateVillaNumber(int number, [FromBody] UpdateVillaNumberDto villadto)
        {
            try
            {
                if (villadto == null || number != villadto.VillaNo)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                if (await _villaRepository.GetAsync(u => u.Id == villadto.VillaId) == null)
                {
                    ModelState.AddModelError("createdError", "Villa ID is Invalid!");
                    return BadRequest(ModelState);
                }
                var villanumbermodel = _mapper.Map<VillaNumber>(villadto);
                await _villanumberrepo.UpdateAsync(villanumbermodel);
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
        [HttpPatch("{number:int}", Name = "UpdatePertialVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePertialVillaNumber(int number, JsonPatchDocument<UpdateVillaNumberDto> PatchDto)
        {
            if (PatchDto == null || number == 0)
            {
                return BadRequest();
            }
            var villanumber = await _villanumberrepo.GetAsync(a => a.VillaNo == number);
            var villanumberdto = _mapper.Map<UpdateVillaNumberDto>(villanumber);
            if (villanumber == null)
            {
                return BadRequest();
            }
            PatchDto.ApplyTo(villanumberdto);
            var villaNumbermodel = _mapper.Map<VillaNumber>(villanumberdto);
            await _villanumberrepo.UpdateAsync(villaNumbermodel);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return NoContent();


        }


    }
}

using System.Security.Principal;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VillaAPI.Data;
using VillaAPI.Dtos;
using VillaAPI.Models;

namespace VillaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public VillaAPIController(ApplicationDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
         
        [HttpGet]
        //Documnt Response 
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ReadVillaDto>>> GetVillas()
        {
            IEnumerable<Villa> VillaList = await _dbContext.Villas.ToListAsync();
            var mappedvilla = _mapper.Map<ReadVillaDto>(VillaList);
            return Ok();
        }
        [HttpGet("id", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReadVillaDto>> GetVillaById(int id)
        {
            if (id == 0)
                return BadRequest();
            var villa = await _dbContext.Villas.FirstOrDefaultAsync(a => a.Id == id);
            if (villa == null)
                return NotFound();
            return Ok(_mapper.Map<ReadVillaDto>(villa));
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReadVillaDto>> AddVilla([FromBody] AddVillaDto createdvilla)
        {
            if (await _dbContext.Villas.FirstOrDefaultAsync(u => u.Name.ToLower() == createdvilla.Name.ToLower()) != null)
            {
                ModelState.AddModelError("createdError", "Villa name  is already Exists");
                return BadRequest(ModelState);
            }
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);
            if (createdvilla == null)
                return BadRequest(createdvilla);
          var villamodel =  _mapper.Map<Villa>(createdvilla);
            await _dbContext.Villas.AddAsync(villamodel);
            await _dbContext.SaveChangesAsync();
            return CreatedAtRoute("GetVilla", new { id = villamodel.Id }, villamodel);


        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("id", Name = "DeleteVilla")]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
                return BadRequest();
            var villa = await _dbContext.Villas.FirstOrDefaultAsync(a => a.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            _dbContext.Villas.Remove(villa);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("id", Name = "UpdateVilla")]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] UpdateVillaDto villadto)
        {
            if (villadto == null || id != villadto.Id)
                return BadRequest();
           var villamodel = _mapper.Map<Villa>(villadto);
            _dbContext.Villas.Update(villamodel);
            await _dbContext.SaveChangesAsync();
            return NoContent();
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
            var villa = await _dbContext.Villas.FirstOrDefaultAsync(a => a.Id == id);
            var villadto = _mapper.Map<UpdateVillaDto>(villa);
            if (villa == null)
            {
                return BadRequest();
            }
            PatchDto.ApplyTo(villadto);
            var villamodel = _mapper.Map<Villa>(villadto);
            _dbContext.Update(villamodel);
           await  _dbContext.SaveChangesAsync();
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return NoContent();


        }


    }
}
 
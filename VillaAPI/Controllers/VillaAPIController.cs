using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using VillaAPI.Data;
using VillaAPI.Dtos;
using VillaAPI.Models;

namespace VillaAPI.Controllers
{
   [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
<<<<<<< HEAD
=======
  

      
>>>>>>> restore-d9bd21be
        [HttpGet]
        //Documnt Response 
        [ProducesResponseType(200,Type =typeof(IEnumerable<ReadVillaDto>))]
        public ActionResult GetVillas() 
        {
               return Ok(VillaStore.VillaList);
        }

        [HttpGet("id",Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ReadVillaDto> GetVillaById(int id)
        {
            if (id == 0)
<<<<<<< HEAD
=======
            {
>>>>>>> restore-d9bd21be
                return BadRequest();
            var villa =  VillaStore.VillaList.FirstOrDefault(a=> a.Id == id);
            if (villa == null)
                return NotFound();
            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ReadVillaDto> AddVilla([FromBody]ReadVillaDto createdvilla) 
        {
            if (VillaStore.VillaList.FirstOrDefault(u => u.Name.ToLower() == createdvilla.Name.ToLower()) != null)
            {
                ModelState.AddModelError("createdError","Villa name  is already Exists");
                return BadRequest(ModelState);
            }
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);
            if (createdvilla == null)
                return BadRequest(createdvilla);
            if (createdvilla.Id > 0)
                return StatusCode(StatusCodes.Status500InternalServerError);
            //Featch the next id
            createdvilla.Id = VillaStore.VillaList
                              .OrderByDescending(a => a.Id)
                              .FirstOrDefault()!.Id+1;
              VillaStore.VillaList.Add(createdvilla);

            return CreatedAtRoute("GetVilla",new {id = createdvilla.Id},createdvilla);

        
        }
<<<<<<< HEAD
=======
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("id", Name = "DeleteVilla")]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
                return BadRequest();
            var villa = VillaStore.VillaList.FirstOrDefault(a => a.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            VillaStore.VillaList.Remove(villa);
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("id", Name = "UpdateVilla")]
        public IActionResult UpdateVilla(int id,[FromBody] ReadVillaDto villadto) 
        {
            if(villadto == null || id != villadto.Id )
                return BadRequest();
            var villa = VillaStore.VillaList.FirstOrDefault(a=> a.Id== id);
            villa.Name = villadto.Name;
            villa.Discription = villadto.Discription;
            return NoContent();
        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPatch("id", Name = "UpdatePartialVilla")]
        public IActionResult UpdatePertialVilla(int id, JsonPatchDocument<ReadVillaDto> PatchDto)
        { 
           if(PatchDto == null || id == 0)
                return BadRequest();
           var villa =  VillaStore.VillaList.FirstOrDefault(a=> a.Id ==id );
            if (villa == null)
                return BadRequest();
            PatchDto.ApplyTo(villa);
                return NoContent();


        }



>>>>>>> restore-d9bd21be

    }
}
 
using VillaAPI.Dtos;
using VillaAPI.Models;

namespace VillaAPI.Data
{
    public static class VillaStore
    {
        public static List<ReadVillaDto> VillaList = new List<ReadVillaDto> 
        {
            new ReadVillaDto { Id = 1 ,Name = "MARASI",Discription = "New"},
            new ReadVillaDto {Id = 2 ,Name = "SHARM",Discription = "Used"}
        };

    
    }
}
 
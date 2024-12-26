using VillaAPI.Dtos;
using VillaAPI.Models;

namespace VillaAPI.Data
{
    public static class VillaStore
    {
        public static List<ReadVillaDto> VillaList = new List<ReadVillaDto> 
        {
            new ReadVillaDto { Id = 1 ,Name = "MARASI"},
            new ReadVillaDto {Id = 2 ,Name = "SHARM"}
        };

    
    }
}
 
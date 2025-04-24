using System.ComponentModel.DataAnnotations;
using Villa_Web.Dtos.VillaDtos;

namespace Villa_Web.Dtos.VillaNumberDtos
{
    public class AddVillaNumberDto
    {
        [Required]
        public int VillaNo { get; set; }
        public string Details { get; set; }
        [Required]
        public int VillaId { get; set; }
        public ReadVillaDto Villa { get; set; }

    }
}

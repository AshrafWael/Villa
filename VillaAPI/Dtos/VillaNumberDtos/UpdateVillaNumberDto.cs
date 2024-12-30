using System.ComponentModel.DataAnnotations;

namespace VillaAPI.Dtos.VillaNumberDtos
{
    public class UpdateVillaNumberDto
    {
        [Required]
        public int VillaNo { get; set; }
        public string Details { get; set; }
        [Required]
        public int VillaId { get; set; }
    }
}

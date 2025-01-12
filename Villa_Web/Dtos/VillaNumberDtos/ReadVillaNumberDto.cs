using System.ComponentModel.DataAnnotations;

namespace Villa_Web.Dtos.VillaNumberDtos
{
    public class ReadVillaNumberDto
    {
        [Required]
        public int VillaNo { get; set; }
        public string Details { get; set; }
        [Required]
        public int VillaId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace VillaAPI.Dtos
{
    public class ReadVillaDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string? Name { get; set; }

    }
}

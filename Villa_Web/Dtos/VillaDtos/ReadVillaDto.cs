using System.ComponentModel.DataAnnotations;

namespace Villa_Web.Dtos.VillaDtos
{
    public class ReadVillaDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Description { get; set; }
        public double Rate { get; set; }
        public string ImageUrl { get; set; }
        public string Amentiy { get; set; }
        public int Sqft { get; set; }
        public int Occupancy { get; set; }

    }
}

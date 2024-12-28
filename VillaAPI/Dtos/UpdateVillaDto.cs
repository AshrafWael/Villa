﻿using System.ComponentModel.DataAnnotations;

namespace VillaAPI.Dtos
{
    public class UpdateVillaDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public Double Rate { get; set; }
        public string ImageUrl { get; set; }
        public string Amentiy { get; set; }
        public int Sqft { get; set; }
        public int Occupancy { get; set; }
        public DateTime UpdatededDtae { get; set; }
    }
}

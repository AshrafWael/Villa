﻿using System.ComponentModel.DataAnnotations;

namespace VillaAPI.Dtos
{
    public class AddVillaDto
    {
        [Required]
        [MaxLength(30)]
        public string? Name { get; set; }
        public string Discription { get; set; }
        public Double Rate { get; set; }
        public string ImageUrl { get; set; }
        public string Amentiy { get; set; }
        public int Sqft { get; set; }
        public int Occupancy { get; set; }
        public DateTime CraetedDtae { get; set; }

    }
}
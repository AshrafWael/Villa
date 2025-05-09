﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VillaAPI.Models
{
    public class Villa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public Double Rate { get; set; } 
        public string ImageUrl { get; set; }
        public string Amentiy { get; set; }
        public int Sqft {  get; set; }
        public int Occupancy { get; set; }
        public DateTime CraetedDtae { get; set; }
        public DateTime UpdatededDtae { get; set; } 
        public VillaNumber VillaNumber { get; set; }


    }
}

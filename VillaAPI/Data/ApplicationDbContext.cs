using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VillaAPI.Models;

namespace VillaAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options) { }
        public DbSet<Villa> Villas { get; set; }
        public DbSet<VillaNumber> villaNumbers { get; set; }
        public DbSet<User>  Users { get; set; }
        public DbSet<ApplicationUser>  ApplicationUsers { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Villa>()
                        .HasData(
              new Villa
                       {
                           Id = 1,
                 Name = "Royal Villa",
                 Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                 ImageUrl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa3.jpg",
                 Occupancy = 4,
                 Rate = 200,
                 Sqft = 550,
                 Amentiy = "",
                 CraetedDtae = DateTime.Now,
              },
              new Villa
              {
                  Id = 2,
                  Name = "Premium Pool Villa",
                  Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                  ImageUrl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa1.jpg",
                  Occupancy = 4,
                  Rate = 300,
                  Sqft = 550,
                  Amentiy = "",
                  CraetedDtae = DateTime.Now,
              },
              new Villa
              {
                  Id = 3,
                  Name = "Luxury Pool Villa",
                  Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                  ImageUrl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa4.jpg",
                  Occupancy = 4,
                  Rate = 400,
                  Sqft = 750,
                  Amentiy = "",
                  CraetedDtae = DateTime.Now,
              }
                );

        }
    }
}

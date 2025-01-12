using Microsoft.AspNetCore.Identity;

namespace Villa_Web.Models
{
    public class ApplicationUser :IdentityUser 
    {
        public string Name { get; set; }
    }
}

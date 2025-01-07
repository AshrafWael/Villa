using VillaAPI.Models;

namespace VillaAPI.Dtos.AccountUserDtos
{
    public class LoginResponseDto
    {
       public ApplicationUserDto User { get; set; }

        public string Token { get; set; }

    }
}

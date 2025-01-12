using Villa_Web.Models;

namespace Villa_Web.Dtos.AccountUserDtos
{
    public class LoginResponseDto
    {
       public ApplicationUserDto User { get; set; }

        public string Token { get; set; }

    }
}

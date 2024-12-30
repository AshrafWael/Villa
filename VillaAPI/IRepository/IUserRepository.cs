using VillaAPI.Dtos.AccountUserDtos;
using VillaAPI.Models;

namespace VillaAPI.IRepository
{
    public interface IUserRepository
    {
        public bool IsUniqueUser(string username);
        public Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        public Task<User> Register(RegisterRequestDto registerRequestDto);
    }
}

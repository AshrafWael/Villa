using Villa_Web.Dtos.AccountUserDtos;

namespace Villa_Web.Services.IServices
{
    public interface IAuthServices
    {
        Task<T> LoginAsync<T>(LoginRequestDto logindto);
        Task<T> RegisterAsync<T>(RegisterRequestDto registerdto);


    }
}

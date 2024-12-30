using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VillaAPI.Data;
using VillaAPI.Dtos.AccountUserDtos;
using VillaAPI.IRepository;
using VillaAPI.Models;

namespace VillaAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private string secretkey;

        public UserRepository(ApplicationDbContext dbContext ,IConfiguration configuration)
        {
            _dbContext = dbContext;
            secretkey = configuration.GetValue<string>("ApiSettings:SecretKey")!;
        }
        public bool IsUniqueUser(string username)
        {
             var user = _dbContext.Users.FirstOrDefault(u =>u.UserName == username);
            if (user == null) 
            {
            return true;
            }
            return false;
        }


        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u=> u .UserName.ToLower() ==
            loginRequestDto.UserName.ToLower() && u.Password == loginRequestDto.Password);
            if (user == null) 
            {
                return new LoginResponseDto()
                {
                    Token = "",
                    User = null
                };
            
            }
            //generate token 
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretkey);

            var tokendescreptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
               {
                new Claim(ClaimTypes.Name,user.Id.ToString()),
                new Claim(ClaimTypes.Role,user.Role)

               }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new (new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };
                    var token = tokenhandler.CreateToken(tokendescreptor);
            LoginResponseDto loginResponse = new LoginResponseDto()
            {

                Token = tokenhandler.WriteToken(token),
                User = user
            };
            return loginResponse;

        }

        public async Task<User> Register(RegisterRequestDto registerRequestDto)
        {
            User user = new()
            {
                UserName =registerRequestDto.UserName,
                Name = registerRequestDto.Name,
                Role = registerRequestDto.Role,
                Password = registerRequestDto.Password,
            };
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            user.Password = "";
            return user;
            
        }
    }
}

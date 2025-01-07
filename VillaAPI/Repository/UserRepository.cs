using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private string secretkey;

        public UserRepository(ApplicationDbContext dbContext ,IConfiguration configuration,
            UserManager<ApplicationUser> userManager,IMapper mapper,RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
            secretkey = configuration.GetValue<string>("ApiSettings:SecretKey")!;
        }
        public bool IsUniqueUser(string username)
        {
             var user = _dbContext.ApplicationUsers.FirstOrDefault(u =>u.UserName == username);
            if (user == null) 
            {
            return true;
            }
            return false;
        }


        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {

            var user = await _dbContext.ApplicationUsers.FirstOrDefaultAsync(u=>
               u.Email ==loginRequestDto.Email || u.UserName.ToUpper() == loginRequestDto.Username.ToUpper());
            var email = _userManager.GetEmailAsync(user);
            bool IsValid = await _userManager.CheckPasswordAsync(user,loginRequestDto.Password);
            if (user == null || IsValid == false) 
            {
                return new LoginResponseDto()
                {
                    Token = "",
                    User = null
                };
            
            }
            //generate token 

            var Roles = await _userManager.GetRolesAsync(user);
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretkey);

            var tokendescreptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
               {
                new Claim(ClaimTypes.Name,user.Email.ToString()),
                new Claim(ClaimTypes.Role,Roles.FirstOrDefault())

               }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new (new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };
                    var token = tokenhandler.CreateToken(tokendescreptor);
            LoginResponseDto loginResponse = new LoginResponseDto()
            {

                Token = tokenhandler.WriteToken(token),
                User = _mapper.Map<ApplicationUserDto>(user),
               // Role = Roles.FirstOrDefault(),
            };
            return loginResponse;

        }

        public async Task<ApplicationUserDto> Register(RegisterRequestDto registerRequestDto)
        {
            ApplicationUser user = new()
            {
                Email = registerRequestDto.Email,
                NormalizedEmail= registerRequestDto.Email.ToUpper(),
                UserName =registerRequestDto.UserName,
                Name = registerRequestDto.Name,
             };
            try
            {
                var result = await _userManager.CreateAsync(user,registerRequestDto.Password);
                if (result.Succeeded) 
                {
                    if (!_roleManager.RoleExistsAsync("admin").GetAwaiter().GetResult())
                    {
                        await _roleManager.CreateAsync(new IdentityRole("admin"));
                    }
                    await _userManager.AddToRoleAsync(user, "admin");
                        var usertoreturn =await _dbContext.ApplicationUsers
                        .FirstOrDefaultAsync(u =>
                        u.Email == registerRequestDto.Email );
                    return _mapper.Map<ApplicationUserDto>(usertoreturn);
                }
            }
            catch (Exception ex)
            { }
            return new ApplicationUserDto();
            
        }
    }
}

using Microsoft.AspNetCore.Http;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using VillaAPI.Dtos.AccountUserDtos;
using VillaAPI.IRepository;
using VillaAPI.Responses;

namespace VillaAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersionNeutral]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        protected APIResponse _response;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _response = new();
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<IActionResult> login([FromBody] LoginRequestDto loginRequestDto)
        {
            var loginresponse = await _userRepository.Login(loginRequestDto);
            if (loginresponse.User == null || string.IsNullOrEmpty(loginresponse.Token))
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Errors.Add("UserName Or Password Is Incorrect");
                return BadRequest(_response);
            }
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = loginresponse;
            return Ok(_response);

        }
        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            bool usernameuniqu = _userRepository.IsUniqueUser(registerRequestDto.UserName);
            if (!usernameuniqu)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Errors.Add("UserNmae Is Already Exist");
                return BadRequest(_response);
            }
            var user = await _userRepository.Register(registerRequestDto);
            if (user == null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Errors.Add("Error While Registration");
                return BadRequest(_response);
            }
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = user;
            return Ok(_response);
        }
    }
}

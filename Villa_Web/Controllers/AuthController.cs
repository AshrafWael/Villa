using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Villa_Utility;
using Villa_Web.Dtos.AccountUserDtos;
using Villa_Web.Responses;
using Villa_Web.Services.IServices;

namespace Villa_Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthServices _authServices;
        private readonly IMapper _Mapper;

        public AuthController(IAuthServices authServices, IMapper mapper)
        {
            _authServices = authServices;
            _Mapper = mapper;
        }
        [HttpGet]
        public IActionResult Login() 
        {
            LoginRequestDto loginRequestDto = new ();
            return View(loginRequestDto);
        
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequestDto loginrequestdto)
        {
            APIResponse response = await _authServices.LoginAsync<APIResponse>(loginrequestdto);
            if (response != null && response.IsSuccess)
            {
                LoginResponseDto loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>
                        (Convert.ToString(response.Result));
                HttpContext.Session.SetString(StaticDta.Sessionkey, loginResponseDto.Token);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("customError", response.Errors.FirstOrDefault());
                return View(loginrequestdto);
            }
           

        }
        [HttpGet]
        public IActionResult Register()
        {
            RegisterRequestDto registerRequestDto = new();
            return View(registerRequestDto);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterRequestDto  registerRequestDto)
        {
         APIResponse result =   await _authServices.RegisterAsync<APIResponse>(registerRequestDto);
            if (result != null && result.IsSuccess) 
            {
            return RedirectToAction("Login");
            }
            return View(result);

        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString(StaticDta.Sessionkey, "");
            return RedirectToAction("Index", "Home");

            return View();

        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();

        }
    }
}

using Microsoft.AspNetCore.Mvc;
using MyWebAppApi.DTOs;
using MyWebAppApi.Services.Interfaces;

namespace MyWebAppApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IUserServices userServices,ILogger<AccountController> logger)
        {
            _userServices = userServices;
            _logger = logger;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Registration(RegisterRequestDto requestDto)
        {

            var response = await _userServices.RegisterUser(requestDto);
            return Ok(response);

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto requestDto)
        {
            var response = await _userServices.LoginUser(requestDto);
            return Ok(response);
        }

        [HttpPatch("password/{id}")]
        public async Task<IActionResult> ChangePassword(int id, PasswordChangeDto passwordChangeDto)
        {
            var response = await _userServices.ChangePassword(id, passwordChangeDto.OldPassword, passwordChangeDto.NewPassword);
            return Ok(response);
        }
    }
}

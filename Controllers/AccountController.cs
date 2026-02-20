using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebAppApi.DTOs;
using MyWebAppApi.Helper;
using MyWebAppApi.Services.Interfaces;

namespace MyWebAppApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly ILogger<AccountController> _logger;
        private readonly IUserFinder _userFinder;

        public AccountController(IUserServices userServices,ILogger<AccountController> logger,IUserFinder userFinder)
        {
            _userServices = userServices;
            _logger = logger;
            _userFinder = userFinder;

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

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var id = _userFinder.GetId();
            _logger.LogInformation("User {userId} logged out", id);
            return Ok(ApiResponseBuilder.Success<string>(null!,"Logged out successfully"));
        }

        [Authorize]
        [HttpPatch("password")]
        public async Task<IActionResult> ChangePassword(PasswordChangeDto passwordChangeDto)
        {
            var response = await _userServices.ChangePassword(passwordChangeDto.OldPassword, passwordChangeDto.NewPassword);
            return Ok(response);
        }
    }
}

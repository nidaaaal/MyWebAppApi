using Microsoft.AspNetCore.Authorization;
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
            var token = response.Data?.Token;

            if (response.IsSuccess && token != null)
            {
                Response.Cookies.Append("jwt", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.UtcNow.AddDays(7)

                });
            };

            return Ok(response);
        }


        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
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

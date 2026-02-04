using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebAppApi.DTOs;
using MyWebAppApi.Services.Interfaces;

namespace MyWebAppApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Registration(RegisterRequestDto requestDto)
        {

          var response = await  _userServices.RegisterUser(requestDto);
            return Ok(response);

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto requestDto)
        {
            var response = await _userServices.LoginUser(requestDto);
            return Ok(response);
        }

        [HttpGet("profile/{id}")]
        public async Task<IActionResult> GetUserProfile(int id)
        {
            var response = await _userServices.GetUserProfile(id);
            return Ok(response);
        }

        [HttpPut("profile/{id}")]
        public async Task<IActionResult> UpdateUserProfile(int id,UpdateProfileDto updateProfileDto)
        {
            var response = await _userServices.UpdateUserProfile(id,updateProfileDto);
            return Ok(response);
        }

    }
}

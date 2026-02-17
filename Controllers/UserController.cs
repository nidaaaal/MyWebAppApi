using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebAppApi.DTOs;
using MyWebAppApi.Services.Interfaces;

namespace MyWebAppApi.Controllers
{
    [Authorize]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;

        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var response = await _userServices.GetUserProfile();
            return Ok(response);
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateUserProfile(UpdateProfileDto updateProfileDto)
        {
            var response = await _userServices.UpdateUserProfile(updateProfileDto);
            return Ok(response);
        }

        [HttpPost("profile/image")]
        public async Task<IActionResult> Update([FromForm] ProfileImageDto profileImageDto)
        {
            var response = await _userServices.UpdateImage(profileImageDto.File);

            return Ok(response);
        }


    }
}

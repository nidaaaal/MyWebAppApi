using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebAppApi.DTOs;
using MyWebAppApi.Helper;
using MyWebAppApi.Services.Interfaces;

namespace MyWebAppApi.Controllers
{
    [Authorize]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IUserFinder _userFinder;

        public UserController(IUserServices userServices, IUserFinder userFinder)
        {
            _userServices = userServices;
            _userFinder = userFinder;

        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            int id = _userFinder.GetId();

            var response = await _userServices.GetUserProfile(id);
            return Ok(response);
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateUserProfile(UpdateProfileDto updateProfileDto)
        {
            int id = _userFinder.GetId();

            var response = await _userServices.UpdateUserProfile(id,updateProfileDto,"User");
            return Ok(response);
        }

        [HttpPost("profile/image")]
        public async Task<IActionResult> Update([FromForm] ProfileImageDto profileImageDto)
        {
            int id = _userFinder.GetId();

            var response = await _userServices.UpdateImage(id,profileImageDto.File,"User");

            return Ok(response);
        }


    }
}

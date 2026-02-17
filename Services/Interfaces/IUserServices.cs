using MyWebApp.Models;
using MyWebAppApi.DTOs;

namespace MyWebAppApi.Services.Interfaces
{
    public interface IUserServices
    {
        Task<ApiResponse<string>> RegisterUser(RegisterRequestDto userRegisterDto);
        Task<ApiResponse<AuthResponseDto>> LoginUser(LoginRequestDto loginRequestDto);

        Task<ApiResponse<Users?>> GetUserProfile();

        Task<ApiResponse<string>> UpdateUserProfile(UpdateProfileDto updateProfileDto);

        Task<ApiResponse<string>> ChangePassword(string oldpassword, string password);

        Task<ApiResponse<string>> UpdateImage(IFormFile file);

    }
}

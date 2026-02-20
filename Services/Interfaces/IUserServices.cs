using MyWebApp.Models;
using MyWebAppApi.DTOs;

namespace MyWebAppApi.Services.Interfaces
{
    public interface IUserServices
    {
        Task<ApiResponse<string>> RegisterUser(RegisterRequestDto userRegisterDto);
        Task<ApiResponse<AuthResponseDto>> LoginUser(LoginRequestDto loginRequestDto);

        Task<ApiResponse<Users?>> GetUserProfile(int id);

        Task<ApiResponse<string>> UpdateUserProfile(int id,UpdateProfileDto updateProfileDto, string role);

        Task<ApiResponse<string>> ChangePassword(string oldpassword, string password);

        Task<ApiResponse<ProfileImageDto>> GetCurrentProfilePath(int id);

        Task<ApiResponse<string>> UpdateImage(int id,IFormFile file, string role);

    }
}

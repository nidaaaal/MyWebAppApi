using MyWebApp.Models;
using MyWebAppApi.DTOs;

namespace MyWebAppApi.Services.Interfaces
{
    public interface IUserServices
    {
        Task<ApiResponse<string>> RegisterUser(RegisterRequestDto userRegisterDto);
        Task<ApiResponse<string>> LoginUser(LoginRequestDto loginRequestDto);

        Task<ApiResponse<Users?>> GetUserProfile(int id);

        Task<ApiResponse<string>> UpdateUserProfile(int id,UpdateProfileDto updateProfileDto);
    }
}

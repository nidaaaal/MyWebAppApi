using MyWebAppApi.DTOs;

namespace MyWebAppApi.Services.Interfaces
{
    public interface IUserServices
    {
        Task<ApiResponse<string>> RegisterUser(RegisterRequestDto userRegisterDto);
        Task<ApiResponse<string>> LoginUser(LoginRequestDto loginRequestDto);
    }
}

using Microsoft.AspNetCore.Identity.Data;
using MyApp.Models;
using MyWebApp.Models;
using MyWebAppApi.DTOs;

namespace MyWebAppApi.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<int> RegisterUser(RegisterRequestDto dto, string hashedpass, int age);
        Task<Credential?> GetUserByUsername(string username);
        Task SaveLogin(int id);
        Task<Users?> GetUserProfile(int id);
        Task<DbResponse> UpdateUserProfile(int id,UpdateProfileDto updateProfileDto,int age);

    }
}

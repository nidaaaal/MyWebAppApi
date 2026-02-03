using Microsoft.AspNetCore.Identity.Data;
using MyApp.Models;
using MyWebAppApi.DTOs;

namespace MyWebAppApi.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<int> RegisterUser(RegisterRequestDto dto, string hashedpass, int age);
        Task<Credential?> GetUserByUsername(string username);
    }
}

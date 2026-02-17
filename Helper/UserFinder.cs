using Microsoft.AspNetCore.Http;
using MyWebAppApi.DTOs;

namespace MyWebAppApi.Helper
{
    public class UserFinder : IUserFinder
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserFinder(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetId()
        {
            if ( _httpContextAccessor.HttpContext?.Items["userId"] is int userId)
            {
                return userId;
            }

            throw new Exception("No User found on The corresponding Id");
        }
    }
}
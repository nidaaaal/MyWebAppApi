using MyWebApp.Models;

namespace MyWebAppApi.Helper
{
    public interface IJwtHelper
    {
        string GetJwtToken(Users user);

    }
}

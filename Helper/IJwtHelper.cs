using MyApp.Models;

namespace MyWebAppApi.Helper
{
    public interface IJwtHelper
    {
        string GetJwtToken(Credential user);

    }
}

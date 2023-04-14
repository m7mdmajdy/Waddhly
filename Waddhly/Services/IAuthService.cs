using Waddhly.Models.Authentication;

namespace Waddhly.Services
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> loginAsync(LoginModel model);
    }
}

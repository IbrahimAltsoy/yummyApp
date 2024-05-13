using yummyApp.Application.Services.Account.Models;

namespace yummyApp.Application.Services.Account
{
    public interface IAccountService
    {
        Task<AuthenticationResponse?> AuthenticateAsync(AuthenticationRequest request);

        Task LogoutAsync();

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<AuthenticationResponse?> RegisterAsync(RegisterRequest request);

        Task<string?> GetUserNameAsync(string userId);

        Task<bool> ActivateUserAsync(string userId, string code);

        Task<bool> IsUserExist(string email);
    }
}

using yummyApp.Application.Dtos.Users;

namespace yummyApp.Application.Services.Authencation
{
    public interface IInternalAuthencation
    {
        Task<Token> LoginAsync(string userNameOrEmail, string password, int accessTokenLifeTime);
        Task<Token> RefreshTokenLoginAsync(string refreshToken);
    }
}

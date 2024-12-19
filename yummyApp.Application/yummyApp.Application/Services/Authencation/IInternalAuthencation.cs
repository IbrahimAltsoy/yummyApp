using yummyApp.Application.Dtos.Users;
using yummyApp.Application.Features.Users.Commands.Register;

namespace yummyApp.Application.Services.Authencation
{
    public interface IInternalAuthencation
    {
        Task<Token> LoginAsync(string userNameOrEmail, string password, int accessTokenLifeTime);
        Task<Token> RefreshTokenLoginAsync(string refreshToken);
        Task<RegisterCommandResponse> RegisterAsync(RegisterCommandRequest request);
    }
}

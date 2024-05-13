using yummyApp.Application.Dtos.Users;

namespace yummyApp.Application.Services.Authencation
{
    public interface IExternalAuthencation
    {
        Task<Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime);
    }
}

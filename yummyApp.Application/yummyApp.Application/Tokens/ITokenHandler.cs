using yummyApp.Application.Abstract.Common;
using yummyApp.Application.Dtos.Users;
using yummyApp.Domain.Identity;

namespace yummyApp.Application.Tokens
{
    public interface ITokenHandler
    {
        Token CreateAccessToken(int second, AppUser user);
        string CreateRefreshToken();
    }
}

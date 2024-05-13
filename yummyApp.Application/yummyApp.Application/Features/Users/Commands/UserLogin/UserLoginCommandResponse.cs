using yummyApp.Application.Dtos.Users;
namespace yummyApp.Application.Features.Users.Commands.UserLogin
{
    public class UserLoginCommandResponse
    {
        public Token AccessToken { get; set; }
        public string Message { get; set; }
        public string UserName { get; set; }
    }
}
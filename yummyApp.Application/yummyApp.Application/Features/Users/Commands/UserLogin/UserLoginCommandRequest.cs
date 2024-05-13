using MediatR;

namespace yummyApp.Application.Features.Users.Commands.UserLogin
{
    public class UserLoginCommandRequest:IRequest<UserLoginCommandResponse>
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
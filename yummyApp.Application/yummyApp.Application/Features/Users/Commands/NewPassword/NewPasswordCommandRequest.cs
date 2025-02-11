using MediatR;

namespace yummyApp.Application.Features.Users.Commands.NewPassword
{
    public class NewPasswordCommandRequest:IRequest<NewPasswordCommandResponse>
    {
        public string UserId { get; set; }
        public string? NewPassword { get; set; }
        public string? Token { get; set; }
    }
}

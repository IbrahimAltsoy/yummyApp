using MediatR;

namespace yummyApp.Application.Features.Users.Commands.PasswordReset
{
    public class PasswordResetCommandRequest:IRequest<PasswordResetCommandResponse>
    {
        public string Email { get; set; }
    }
}
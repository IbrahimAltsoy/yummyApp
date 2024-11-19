using MediatR;

namespace yummyApp.Application.Features.Users.Commands.VerifyEmail
{
    public class VerifyEmailCommandRequest:IRequest<VerifyEmailCommandResponse>
    {       
        public string Email { get; set; }
        public string ActivationCode { get; set; }
    }
}

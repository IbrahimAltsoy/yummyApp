using MediatR;

namespace yummyApp.Application.Features.Users.Commands.VerifyResetToken
{
    public class VerifyResetTokenCommandRequest : IRequest<VerifyResetTokenCommandResponse>
    {
        public Guid Id { get; set; }
        public string ResetToken { get; set; }
    }
}
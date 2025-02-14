using MediatR;

namespace yummyApp.Application.Features.Users.Commands.Delete
{
    public class DeleteUserCommandRequest:IRequest<DeleteUserCommandResponse>
    {
        public Guid Id { get; set; }
    }
}

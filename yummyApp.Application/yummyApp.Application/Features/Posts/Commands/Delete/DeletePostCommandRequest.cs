using MediatR;

namespace yummyApp.Application.Features.Posts.Commands.Delete
{
    public class DeletePostCommandRequest:IRequest<DeletePostCommandResponse>
    {
        public Guid Id { get; set; }
    }
}
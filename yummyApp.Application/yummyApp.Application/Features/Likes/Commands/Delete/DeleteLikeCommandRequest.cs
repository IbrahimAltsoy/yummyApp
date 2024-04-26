using MediatR;

namespace yummyApp.Application.Features.Likes.Commands.Delete
{
    public class DeleteLikeCommandRequest:IRequest<DeleteLikeCommandResponse>
    {
        public Guid Id { get; set; }
    }
}
using MediatR;

namespace yummyApp.Application.Features.Comments.Commands.Delete
{
    public class DeleteCommentCommandRequest:IRequest<DeleteCommentCommandResponse>
    {
        public Guid Id { get; set; }
    }
}

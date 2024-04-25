using MediatR;
using yummyApp.Domain.Entities;

namespace yummyApp.Application.Features.Comments.Commands.Create
{
    public class CreateCommentCommandRequest:IRequest<CreateCommentCommandResponse>
    {
        public Guid? UserID { get; set; }
        public Guid? PostID { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

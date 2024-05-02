using MediatR;
using yummyApp.Domain.Entities;

namespace yummyApp.Application.Features.Comments.Commands.Create
{
    public class CreateCommentCommandRequest:IRequest<CreateCommentCommandResponse>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int LikeCount { get; set; }
        public DateTime Timestamp { get; set; }

        public Guid? UserID { get; set; }
        public Guid? PostID { get; set; }
    }
}

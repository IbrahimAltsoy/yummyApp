using MediatR;
using yummyApp.Domain.Enums;

namespace yummyApp.Application.Features.Posts.Commands.Create
{
    public class CreatePostCommandRequest : IRequest<CreatePostCommandResponse>
    {
        public Guid? UserID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public PostQuality Quality { get; set; }
        public int Rating { get; set; }
    }
}

using MediatR;

namespace yummyApp.Application.Features.Likes.Commands.Create
{
    public class CreateLikecommandRequest : IRequest<CreateLikecommandResponse>
    {
        public Guid? UserID { get; set; }
        public Guid? PostID { get; set; }
    }
}
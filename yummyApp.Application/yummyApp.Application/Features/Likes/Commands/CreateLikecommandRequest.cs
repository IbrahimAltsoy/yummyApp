using MediatR;

namespace yummyApp.Application.Features.Likes.Commands
{
    public class CreateLikecommandRequest:IRequest<CreateLikecommandResponse>
    {
        public Guid? UserID { get; set; }
        public Guid? PostID { get; set; }
    }
}
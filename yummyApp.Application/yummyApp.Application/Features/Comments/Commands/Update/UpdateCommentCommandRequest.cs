using MediatR;

namespace yummyApp.Application.Features.Comments.Commands.Update
{
    public class UpdateCommentCommandRequest:IRequest<UpdateCommentCommandResponse>
    {
        
        public Guid Id { get; set; }
        //public Guid PostID { get; set; }
        //public Guid USerID { get; set; }
        public string Content { get; set; }
        
    }
}

using MediatR;

namespace yummyApp.Application.Features.Tags.Commands.Update
{
    public class UpdateTagCommandRequest:IRequest<UpdateTagCommandResponse>
    {
        public Guid Id { get; set; }
        public Guid? PostID { get; set; }
        public Guid? BusinessID { get; set; }
    }
}
using MediatR;

namespace yummyApp.Application.Features.Tags.Commands.Create
{
    public class CreateTagCommandRequest:IRequest<CreateTagCommandResponse>
    {
        public Guid? PostID { get; set; }
        public Guid? BusinessID { get; set; }
    }
}
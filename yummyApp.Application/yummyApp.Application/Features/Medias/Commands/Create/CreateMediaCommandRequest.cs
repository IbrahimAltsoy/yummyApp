using MediatR;

namespace yummyApp.Application.Features.Medias.Commands.Create
{
    public class CreateMediaCommandRequest:IRequest<CreateMediaCommandResponse>
    {
        public List<string> Urls { get; set; }
        public Guid PostId { get; set; }
    }
}
using MediatR;

namespace yummyApp.Application.Features.Medias.Commands.Update
{
    public class UpdateMediaCommandRequest:IRequest<UpdateMediaCommandResponse>
    {
        public Guid Id { get; set; }
        public string NewUrl { get; set; }
    }
}
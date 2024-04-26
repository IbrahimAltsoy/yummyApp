using MediatR;

namespace yummyApp.Application.Features.Medias.Commands.Delete
{
    public class DeleteMediaCommandRequest:IRequest<DeleteMediaCommandResponse>
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        
    }
}
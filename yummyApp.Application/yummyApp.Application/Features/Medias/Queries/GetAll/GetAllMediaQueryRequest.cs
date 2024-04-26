using MediatR;

namespace yummyApp.Application.Features.Medias.Queries.GetAll
{
    public class GetAllMediaQueryRequest:IRequest<GetAllMediaQueryResponse>
    {
        public Guid Id { get; set; }
    }
}
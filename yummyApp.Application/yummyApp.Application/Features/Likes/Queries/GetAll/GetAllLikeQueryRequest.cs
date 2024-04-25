using MediatR;

namespace yummyApp.Application.Features.Likes.Queries.GetAll
{
    public class GetAllLikeQueryRequest:IRequest<List<GetAllLikeQueryResponse>>
    {
        public Guid PostId { get; set; }
    }
}
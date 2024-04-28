using MediatR;

namespace yummyApp.Application.Features.Posts.Queries.GetAll
{
    public class GetAllPostQueryRequest:IRequest<IList<GetAllPostQueryResponse>>
    {
    }
}
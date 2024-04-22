using MediatR;

namespace yummyApp.Application.Features.Businesses.Queries.GetAll
{
    public class GetAllBusinessQueryRequest:IRequest<IList<GetAllBusinessQueryResponse>>
    {
    }
}

using MediatR;

namespace yummyApp.Application.Features.Businesses.Queries.GetById
{
    public class GetByIdQueryRequest:IRequest<GetByIdQueryResponse>
    {
        public Guid Id { get; set; }
    }
}

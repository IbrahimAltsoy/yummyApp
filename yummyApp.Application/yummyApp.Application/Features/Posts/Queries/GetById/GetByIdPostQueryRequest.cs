using MediatR;

namespace yummyApp.Application.Features.Posts.Queries.GetById
{
    public class GetByIdPostQueryRequest:IRequest<GetByIdPostQueryResponse>
    {
        public Guid Id { get; set; }
    }
}
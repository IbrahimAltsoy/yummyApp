using MediatR;

namespace yummyApp.Application.Features.UserFeedBacks.Queries.GetById
{
    public class GetByIdUserFeedBackQueryRequest:IRequest<GetByIdUserFeedBackQueryResponse>
    {
        public Guid Id { get; set; }
    }
}

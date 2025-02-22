using MediatR;

namespace yummyApp.Application.Features.UserFeedBacks.Queries.GetById
{
    public class GetByIdUserFeedBackQueryHandler : IRequestHandler<GetByIdUserFeedBackQueryRequest, GetByIdUserFeedBackQueryResponse>
    {
        public Task<GetByIdUserFeedBackQueryResponse> Handle(GetByIdUserFeedBackQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

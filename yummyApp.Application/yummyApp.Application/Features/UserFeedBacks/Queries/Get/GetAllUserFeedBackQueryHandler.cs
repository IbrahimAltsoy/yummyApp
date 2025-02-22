using MediatR;

namespace yummyApp.Application.Features.UserFeedBacks.Queries.Get
{
    public class GetAllUserFeedBackQueryHandler : IRequestHandler<GetAllUserFeedBackQueryRequest, GetAllUserFeedBackQueryResponse>
    {
        public Task<GetAllUserFeedBackQueryResponse> Handle(GetAllUserFeedBackQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

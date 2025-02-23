using MediatR;

namespace yummyApp.Application.Features.UserFeedBacks.Queries.Get
{
    public class GetAllUserFeedBackQueryRequest:IRequest<GetAllUserFeedBackQueryResult>
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 10;
    }
}

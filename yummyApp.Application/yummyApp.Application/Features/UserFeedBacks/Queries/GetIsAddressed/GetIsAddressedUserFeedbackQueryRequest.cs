using MediatR;
using yummyApp.Application.Features.UserFeedBacks.Queries.Get;

namespace yummyApp.Application.Features.UserFeedBacks.Queries.GetIsAddressed
{
    public class GetIsAddressedUserFeedbackQueryRequest : IRequest<GetAllUserFeedBackQueryResult>
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 10;
    }
}

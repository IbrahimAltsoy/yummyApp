using MediatR;

namespace yummyApp.Application.Features.FriendShips.Queries.GetAll
{
    public class GetAllFrindShipQueryRequst:IRequest<GetAllFrindShipQueryResponse>
    {
        public Guid Id { get; set; }
    }
}
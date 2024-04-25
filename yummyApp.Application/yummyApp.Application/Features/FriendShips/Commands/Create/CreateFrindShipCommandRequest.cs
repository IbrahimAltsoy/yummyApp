using MediatR;

namespace yummyApp.Application.Features.FriendShips.Commands.Create
{
    public class CreateFrindShipCommandRequest:IRequest<CreateFrindShipCommandResponse>
    {
        public Guid? FollowerID { get; set; }
        public Guid? FolloweeID { get; set; }
        public bool FriendshipStatus { get; set; }
    }
}
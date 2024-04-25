namespace yummyApp.Application.Features.FriendShips.Queries.GetAll
{
    public class GetAllFrindShipQueryResponse
    {
        //public Guid? FollowerID { get; set; }
        //public Guid? FolloweeID { get; set; }
        public bool FriendshipStatus { get; set; }
        public int Follower {  get; set; }
        public int Followee { get; set;}
    }
}
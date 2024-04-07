using yummyApp.Application.Repositories;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Domain.Entities;
using yummyApp.Persistance.Context;

namespace yummyApp.Persistance.Repositories
{
    public class FriendShipRepository : EfRepositoryBase<Friendship, Guid, YummyAppDbContext>, IFriendShipRepository
    {
        public FriendShipRepository(YummyAppDbContext context) : base(context)
        {
        }
    }
}

using yummyApp.Domain.Entities;

namespace yummyApp.Application.Repositories.Repository
{
    public interface IFriendShipRepository: IAsyncRepository<Friendship, Guid>, IRepository<Friendship, Guid> { }
}

using yummyApp.Domain.Entities;
using yummyApp.Domain.Identity;

namespace yummyApp.Application.Repositories.Repository
{
    public interface ILikeRepository: IAsyncRepository<Like, Guid>, IRepository<Like, Guid> 
    
    { Task<List<AppUser>> UsersWhoLikedAsync(Guid postId); }
    
}

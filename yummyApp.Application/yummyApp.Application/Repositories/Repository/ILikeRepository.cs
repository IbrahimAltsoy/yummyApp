using yummyApp.Domain.Entities;

namespace yummyApp.Application.Repositories.Repository
{
    public interface ILikeRepository: IAsyncRepository<Like, Guid>, IRepository<Like, Guid> { }
}

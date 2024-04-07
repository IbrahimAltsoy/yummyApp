using yummyApp.Domain.Entities;

namespace yummyApp.Application.Repositories.Repository
{
    public interface IPostRepository: IAsyncRepository<Post, Guid>, IRepository<Post, Guid> { }
}

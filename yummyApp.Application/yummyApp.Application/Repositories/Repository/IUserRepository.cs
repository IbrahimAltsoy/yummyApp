using yummyApp.Domain.Entities;

namespace yummyApp.Application.Repositories.Repository
{
    public interface IUserRepository: IAsyncRepository<User, Guid>, IRepository<User, Guid> { }
}

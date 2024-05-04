using yummyApp.Application.Repositories;
using yummyApp.Domain.Entities;
using yummyApp.Domain.Identity;

namespace yummyApp.Application.Repositories.Repository
{
    public interface IUserRepository : IAsyncRepository<AppUser, Guid>, IRepository<AppUser, Guid> { }
}

using yummyApp.Application.Repositories;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Domain.Entities;
using yummyApp.Persistance.Context;

namespace yummyApp.Persistance.Repositories
{
    public class UserRepository : EfRepositoryBase<User, Guid, YummyAppDbContext>, IUserRepository
    {
        public UserRepository(YummyAppDbContext context) : base(context)
        {
        }
    }
}

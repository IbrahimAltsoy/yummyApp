using yummyApp.Application.Repositories.Repository;
using yummyApp.Application.Repositories;
using yummyApp.Domain.Entities;

namespace yummyApp.Persistance.Repositories
{
    public class UserFeedBackRepository : EfRepositoryBase<UserFeedback, Guid, YummyAppDbContext>, IUserFeedBackRepository
    {
        public UserFeedBackRepository(YummyAppDbContext context) : base(context)
        {
        }
    }
}

using yummyApp.Application.Repositories.Repository;
using yummyApp.Application.Repositories;
using yummyApp.Domain.Entities;
using yummyApp.Persistance.Context;

namespace yummyApp.Persistance.Repositories
{
    public class LikeRepository : EfRepositoryBase<Like, Guid, YummyAppDbContext>, ILikeRepository
    {
        public LikeRepository(YummyAppDbContext context) : base(context)
        {
        }
    }
}

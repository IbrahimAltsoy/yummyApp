using yummyApp.Application.Repositories;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Domain.Entities;
using yummyApp.Persistance.Context;

namespace yummyApp.Persistance.Repositories
{
    public class PostRepository : EfRepositoryBase<Post, Guid, YummyAppDbContext>, IPostRepository
    {
        public PostRepository(YummyAppDbContext context) : base(context)
        {
        }
    }
}

using yummyApp.Application.Repositories;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Domain.Entities;
using yummyApp.Persistance.Context;

namespace yummyApp.Persistance.Repositories
{
    public class CommentRepository : EfRepositoryBase<Comment, Guid, YummyAppDbContext>, ICommentRepository
    {
        public CommentRepository(YummyAppDbContext context) : base(context)
        {
        }
    }
}

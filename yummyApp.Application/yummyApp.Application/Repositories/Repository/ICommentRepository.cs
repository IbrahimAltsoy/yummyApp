using yummyApp.Domain.Entities;

namespace yummyApp.Application.Repositories.Repository
{
    public interface ICommentRepository:IAsyncRepository<Comment, Guid>, IRepository<Comment, Guid>{}

    
}

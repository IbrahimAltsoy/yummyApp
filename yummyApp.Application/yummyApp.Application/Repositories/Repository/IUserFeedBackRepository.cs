using yummyApp.Domain.Entities;

namespace yummyApp.Application.Repositories.Repository
{
    public interface IUserFeedBackRepository : IAsyncRepository<UserFeedback, Guid>, IRepository<UserFeedback, Guid> { }
    
}

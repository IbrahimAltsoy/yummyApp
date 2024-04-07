using yummyApp.Domain.Entities;

namespace yummyApp.Application.Repositories.Repository
{
    public interface ITagRepository: IAsyncRepository<Tag, Guid>, IRepository<Tag, Guid> { }
}

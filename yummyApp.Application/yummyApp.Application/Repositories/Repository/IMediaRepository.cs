using yummyApp.Domain.Entities;

namespace yummyApp.Application.Repositories.Repository
{
    public interface IMediaRepository: IAsyncRepository<Media, Guid>, IRepository<Media, Guid> { }
}

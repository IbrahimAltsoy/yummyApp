using yummyApp.Domain.Entities;

namespace yummyApp.Application.Repositories.Repository
{
    public interface IBusinessRepository: IAsyncRepository<Business, Guid>, IRepository<Business, Guid>{}
}

using yummyApp.Application.Repositories;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Domain.Entities;
using yummyApp.Persistance.Context;

namespace yummyApp.Persistance.Repositories
{
    public class BusinessRepository : EfRepositoryBase<Business, Guid, YummyAppDbContext>, IBusinessRepository
    {
        public BusinessRepository(YummyAppDbContext context) : base(context)
        {
        }
    }
}

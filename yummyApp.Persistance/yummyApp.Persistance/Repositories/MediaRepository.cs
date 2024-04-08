using yummyApp.Application.Repositories;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Domain.Entities;
using yummyApp.Persistance.Context;

namespace yummyApp.Persistance.Repositories
{
    public class MediaRepository : EfRepositoryBase<Media, Guid, YummyAppDbContext>, IMediaRepository
    {
        public MediaRepository(YummyAppDbContext context) : base(context){}
    }
}

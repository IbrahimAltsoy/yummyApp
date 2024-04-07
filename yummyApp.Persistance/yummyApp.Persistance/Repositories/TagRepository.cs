using yummyApp.Application.Repositories;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Domain.Entities;
using yummyApp.Persistance.Context;

namespace yummyApp.Persistance.Repositories
{
    public class TagRepository : EfRepositoryBase<Tag, Guid, YummyAppDbContext>, ITagRepository
    {
        public TagRepository(YummyAppDbContext context) : base(context)
        {
        }
    }
}

using Microsoft.EntityFrameworkCore;
using yummyApp.Application.Repositories;
using yummyApp.Application.Repositories.Repository;
using yummyApp.Domain.Entities;
using yummyApp.Persistance.Context;

namespace yummyApp.Persistance.Repositories
{
    public class MediaRepository : EfRepositoryBase<Media, Guid, YummyAppDbContext>, IMediaRepository
    {
        public MediaRepository(YummyAppDbContext context) : base(context){}

        public async Task AddPhotoToPostAsync(Guid id, string newUrl)
        {
            var postMedia = await Context.Medias.FirstOrDefaultAsync(m => m.Id == id);
            if (postMedia != null)
            {
                postMedia.Urls.Add(newUrl);
                await Context.SaveChangesAsync();
            }
        }

        public async Task RemovePhotoFromPost(Guid id, string urlToRemove)
        {
            var postMedia = await Context.Medias.FirstOrDefaultAsync(m => m.Id == id);
            if (postMedia != null)
            {
                if (postMedia.Urls.Contains(urlToRemove))
                {
                    postMedia.Urls.Remove(urlToRemove);
                   await Context.SaveChangesAsync();
                }
            }
        }
       
    }
}

using yummyApp.Domain.Entities;

namespace yummyApp.Application.Repositories.Repository
{
    public interface IMediaRepository: IAsyncRepository<Media, Guid>, IRepository<Media, Guid> 
    {
        Task RemovePhotoFromPost(Guid id, string urlToRemove);
        Task AddPhotoToPostAsync(Guid id, string newUrl);
    }
    
}

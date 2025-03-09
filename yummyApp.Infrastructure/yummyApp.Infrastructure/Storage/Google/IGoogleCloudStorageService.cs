using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace yummyApp.Infrastructure.Storage.Google
{
    public interface IGoogleCloudStorageService
    {
        Task<string> UploadImageAsync(IFormFile file);
        Task<bool> DeleteImageAsync(string imageUrl);
    }
}

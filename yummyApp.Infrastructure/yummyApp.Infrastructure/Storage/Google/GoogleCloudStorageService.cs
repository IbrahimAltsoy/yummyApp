using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace yummyApp.Infrastructure.Storage.Google
{
    public class GoogleCloudStorageService : IGoogleCloudStorageService
    {
        private readonly string _bucketName;
        private readonly StorageClient _storageClient;

        public GoogleCloudStorageService(IConfiguration configuration)
        {
            string credentialsPath = Path.Combine(Directory.GetCurrentDirectory(), configuration["GoogleCloud:CredentialsFilePath"]);
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialsPath);

            _storageClient = StorageClient.Create();
            _bucketName = configuration["GoogleCloud:BucketName"];
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Dosya boş olamaz.");

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            using (var stream = file.OpenReadStream())
            {
                await _storageClient.UploadObjectAsync(_bucketName, fileName, file.ContentType, stream);
            }

            return $"https://storage.googleapis.com/{_bucketName}/{fileName}";
        }
        public async Task<bool> DeleteImageAsync(string imageUrl)
        {
            try
            {
                var fileName = imageUrl.Split('/').Last();
                await _storageClient.DeleteObjectAsync(_bucketName, fileName);
                return true;
            }
            catch (Exception)
            {
                return false; 
            }
        }

    }
}

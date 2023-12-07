using Microsoft.AspNetCore.Http;

namespace CloudStorage.Core
{
    public interface IStorageManager
    {
        Task<string> UploadAsync(IFormFile file);
        Task<string> DownloadAsync(string photoGuid);
    }
}
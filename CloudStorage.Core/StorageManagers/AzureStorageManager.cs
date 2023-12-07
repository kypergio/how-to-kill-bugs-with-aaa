using Microsoft.AspNetCore.Http;
using CloudStorage.Core.Utils;
using CloudStorage.Core.StorageManagers;

namespace CloudStorage.Core
{
    public class AzureStorageManager : IStorageManager
    {
        private readonly IBlobClient _blobClient;

        private readonly IEnvironmentManager _environmentManager;
        private readonly IFileWrapper _fileWrapper;


        public AzureStorageManager(IEnvironmentManager environmentManager, IBlobClient blobClient, IFileWrapper fileWrapper)
        {
            _environmentManager = environmentManager;
            _blobClient = blobClient;
            _fileWrapper = fileWrapper;
        }

        public async Task<string> DownloadAsync(string photoGuid)
        {
            string fileName = FileNameHelper.RemovePathSegments(photoGuid);
            string pathResult = $"{_environmentManager.WebRootPath}/{fileName}";

            if (!_fileWrapper.Exists(pathResult))
            {
                await _blobClient.DownloadAsync(fileName);
            }
            return $"{_environmentManager.ApplicationHost}/{fileName}";
        }


        public async Task<string> UploadAsync(IFormFile file)
        {
            var stream = await SetMemoryStream(file);
            var guid = Guid.NewGuid().ToString();
            var fileName = $"{guid}{FileNameHelper.ExtractExtension(file.FileName)}";

            await _blobClient.UploadAsync(fileName, stream);

            return _blobClient.Uri;
        }

        private async Task<MemoryStream> SetMemoryStream(IFormFile file)
        {
            var stream = new MemoryStream();

            await file.CopyToAsync(stream);

            stream.Position = 0;
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

    }
}


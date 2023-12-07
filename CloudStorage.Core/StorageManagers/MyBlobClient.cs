using Azure.Storage.Blobs;
using CloudStorage.Core.Utils;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudStorage.Core.StorageManagers
{
    public class MyBlobClient: IBlobClient
    {
        IBlobServiceClient _blobServiceClient;

        public string Uri { get; set; } = string.Empty;

        public MyBlobClient(IBlobServiceClient serviceClient) => _blobServiceClient = serviceClient;

        public async Task DownloadAsync(string fileName)
        {
            BlobClient client = _blobServiceClient.GetBlobClient(fileName);
            using FileStream fs = new(fileName, FileMode.Create);
            await client.DownloadToAsync(fs);
        }

        public async Task UploadAsync(string fileName, MemoryStream stream)
        {
            BlobClient client = _blobServiceClient.GetBlobClient(fileName);
            await client.UploadAsync(stream, true);

            Uri = client.Uri.ToString();
        }
    }
}

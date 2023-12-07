using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudStorage.Core.StorageManagers
{
    public class MyBlobServiceClient: IBlobServiceClient
    {
        private BlobContainerClient _client;
        public MyBlobServiceClient(BlobContainerClient client)
        {
            _client = client;
        }

        public BlobClient GetBlobClient(string fileName)
        {
            return _client.GetBlobClient(fileName);
        }
    }
}

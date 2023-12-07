using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudStorage.Core.StorageManagers
{
    public interface IBlobServiceClient
    {
        public BlobClient GetBlobClient(string fileName);
    }
}

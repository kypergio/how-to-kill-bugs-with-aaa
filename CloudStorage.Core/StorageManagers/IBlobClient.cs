using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudStorage.Core.StorageManagers
{
    public interface IBlobClient
    {
        public string Uri { get; set; }
        public Task DownloadAsync(string fileName);

        public Task UploadAsync(string fileName, MemoryStream stream);
    }
}

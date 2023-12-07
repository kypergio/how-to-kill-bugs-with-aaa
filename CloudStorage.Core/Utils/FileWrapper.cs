using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudStorage.Core.Utils
{
    public class FileWrapper : IFileWrapper
    {
        public bool Exists(string path) => File.Exists(path);
    }
}

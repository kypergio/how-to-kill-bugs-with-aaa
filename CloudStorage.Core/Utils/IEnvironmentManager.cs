using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudStorage.Core.Utils
{
    public interface IEnvironmentManager
    {
        public string WebRootPath { get; }

        public string ApplicationHost { get; }
    }
}

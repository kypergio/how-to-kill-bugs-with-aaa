using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudStorage.Core.Utils
{
    public class EnvironmentManager : IEnvironmentManager
    {
        private readonly IWebHostEnvironment _environment;
        public EnvironmentManager(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public string WebRootPath
        {
            get
            {

                return _environment.WebRootPath;
            }
        }

        public string ApplicationHost
        {
            get
            {
                var url = Environment.GetEnvironmentVariable("ASPNETCORE_URLS")?.Split(";");

                if (url.Length == 0)
                    return "http://localhost";

                return url[0];
            }
        }
    }
}

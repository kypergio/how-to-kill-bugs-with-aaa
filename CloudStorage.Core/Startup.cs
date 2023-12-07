using Azure.Storage.Blobs;
using CloudStorage.Core.StorageManagers;
using CloudStorage.Core.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;

namespace CloudStorage.Core
{
    public static class Startup
    {
        public static IServiceCollection RegisterCloudStorageCore(this IServiceCollection services, string connectionString) => services
            .AddDbContext<DbCloudStorageContext>(
                            o => o.UseSqlite(connectionString,
                            x => x.MigrationsAssembly("CloudStorage.Core")))
            .AddTransient<IPokemonRepository, PokemonSQLiteRepository>()
            .AddTransient<IPokemonService, PokemonService>()
            .AddTransient<IEnvironmentManager, EnvironmentManager>()
            .AddTransient<IStorageManager, AzureStorageManager>()
            .AddTransient<IBlobClient, MyBlobClient>()
            .AddTransient<IFileWrapper, FileWrapper>()
            .AddTransient<IBlobServiceClient, MyBlobServiceClient>((services) =>
            {
                var configuration = services.GetService<IConfiguration>();
                return new MyBlobServiceClient(new BlobServiceClient(configuration.GetSection("Azure:StorageConnectionString").Value)
                                  .GetBlobContainerClient(configuration.GetSection("Azure:ContainerName").Value));
            });
    }
}


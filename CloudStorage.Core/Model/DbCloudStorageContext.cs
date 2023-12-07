using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudStorage.Core
{
    public class DbCloudStorageContext : DbContext
    {
        public DbCloudStorageContext(DbContextOptions<DbCloudStorageContext> options)
            : base(options)
        {
        }

        public DbSet<Pokemon> Pokemons { get; set; }
    }
}

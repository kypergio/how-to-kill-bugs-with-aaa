using CloudStorage.Core.Exceptions;
using CloudStorage.Core.Model;
using CloudStorage.Core.Model.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudStorage.Core
{
    public class PokemonSQLiteRepository : IPokemonRepository
    {
        private readonly DbCloudStorageContext _db;
        public PokemonSQLiteRepository(DbCloudStorageContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Pokemon>> GetAllAsync()
        {
            return await _db.Pokemons.ToListAsync();
        }

        public async Task<Pokemon?> GetAsync(int id)
        {
            return await _db.Pokemons.FindAsync(id);
        }

        public async Task<Pokemon?> AddAsync(Pokemon input)
        {
            try
            {
                await _db.Database.BeginTransactionAsync();

                var result = await _db.Pokemons.AddAsync(input);

                await _db.SaveChangesAsync();

                await _db.Database.CommitTransactionAsync();

                return new Pokemon { Id = result.Entity.Id, Name = result.Entity.Name, Photo = result.Entity.Photo };
            }
            catch (Exception)
            {
                await _db.Database.RollbackTransactionAsync();
                throw;
            }
        }
    }
}

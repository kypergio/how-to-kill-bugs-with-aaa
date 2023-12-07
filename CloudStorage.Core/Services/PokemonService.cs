using CloudStorage.Core.Exceptions;
using CloudStorage.Core.Model;
using CloudStorage.Core.Model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudStorage.Core { 
    public class PokemonService : IPokemonService
    {
        private readonly IPokemonRepository _repository;
        private readonly IStorageManager _storage;

        public PokemonService(IPokemonRepository repository, IStorageManager storage)
        {
            _repository = repository;
            _storage = storage;
        }

        public async Task<PokemonQueryDAL> GetAsync(int id)
        {
            var result = await _repository.GetAsync(id);

            if (result == null)
                throw new EntityNotFoundException();

            var localImage = await _storage.DownloadAsync(result.Photo!);

            return new PokemonQueryDAL
            {
                Id = result.Id,
                Name = result.Name,
                Photo = localImage
            };
        }


        public async Task<IEnumerable<PokemonQueryDAL>> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();

            return result.Select(c => new PokemonQueryDAL
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }

        public async Task<PokemonQueryDAL> AddAsync(PokemonUpsertDAL input)
        {
            var photoName = await _storage.UploadAsync(input.Photo!);

            var result = await _repository.AddAsync(new Pokemon
            {
                Name = input.Name,
                Photo = photoName
            });

            return new PokemonQueryDAL
            {
                Id = result.Id,
                Name = result.Name,
                Photo = photoName
            };
        }
    }
}

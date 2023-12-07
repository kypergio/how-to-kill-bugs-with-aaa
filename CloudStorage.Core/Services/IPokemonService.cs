using CloudStorage.Core.Model.DAL;

namespace CloudStorage.Core
{
    public interface IPokemonService
    {
        Task<PokemonQueryDAL> AddAsync(PokemonUpsertDAL input);
        Task<PokemonQueryDAL> GetAsync(int id);
        Task<IEnumerable<PokemonQueryDAL>> GetAllAsync();
    }
}
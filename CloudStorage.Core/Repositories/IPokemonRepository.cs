using CloudStorage.Core.Model;

namespace CloudStorage.Core
{
    public interface IPokemonRepository
    {
        Task<Pokemon?> AddAsync(Pokemon input);
        Task<IEnumerable<Pokemon>> GetAllAsync();
        Task<Pokemon?> GetAsync(int id);
    }
}
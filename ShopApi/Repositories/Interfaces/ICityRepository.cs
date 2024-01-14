using Models;

namespace Repositories.Interfaces
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> RetrieveAllAsync();
        Task<City?> RetrieveAsync(int id);
        Task<City?> CreateAsync(City data);
        Task<City?> UpdateAsync(int id, City data);
        Task<bool?> DeleteAsync(int id);
    }
}

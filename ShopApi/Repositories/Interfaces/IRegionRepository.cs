using Models;

namespace Repositories.Interfaces
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> RetrieveAllAsync();
        Task<Region?> RetrieveAsync(int id);
        Task<Region?> CreateAsync(Region data);
        Task<Region?> UpdateAsync(int id, Region data);
        Task<bool?> DeleteAsync(int id);
    }
}

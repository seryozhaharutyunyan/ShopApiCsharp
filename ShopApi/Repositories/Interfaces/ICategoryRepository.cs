using Models;

namespace Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> RetrieveAllAsync();
        Task<Category?> RetrieveAsync(int id);
        Task<Category?> CreateAsync(Category data);
        Task<Category?> UpdateAsync(int id, Category data);
        Task<bool?> DeleteAsync(int id);
    }
}

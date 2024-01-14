using Models;

namespace Repositories.Interfaces
{
    public interface ILikeRepository
    {
        Task<IEnumerable<Like>> RetrieveAllAsync();
        Task<Like?> RetrieveAsync(int id);
        Task<Like?> CreateAsync(Like data);
        Task<Like?> UpdateAsync(int id, Like data);
        Task<bool?> DeleteAsync(int id);
    }
}

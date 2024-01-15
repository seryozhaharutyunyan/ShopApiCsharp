using Models;

namespace Repositories.Interfaces
{
    public interface IRepository
    {
        Task<IEnumerable<T>> RetrieveAllAsync<T>();
        Task<T?> RetrieveAsync<T>(int id);
        Task<T?> CreateAsync<T>(T data);
        Task<T?> UpdateAsync<T>(int id, T data);
        Task<bool?> DeleteAsync(int id);
    }
}

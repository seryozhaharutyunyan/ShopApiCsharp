using Models;

namespace Repositories.Interfaces
{
    public interface IEdgeRepository
    {
        Task<IEnumerable<Edge>> RetrieveAllAsync();
        Task<Edge?> RetrieveAsync(int id);
        Task<Edge?> CreateAsync(Edge data);
        Task<Edge?> UpdateAsync(int id, Edge data);
        Task<bool?> DeleteAsync(int id);
    }
}

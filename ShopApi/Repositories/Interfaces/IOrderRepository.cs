using Models;

namespace Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> RetrieveAllAsync();
        Task<Order?> RetrieveAsync(int id);
        Task<Order?> CreateAsync(Order data);
        Task<Order?> UpdateAsync(int id, Order data);
        Task<bool?> DeleteAsync(int id);
    }
}

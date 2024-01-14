using Models;

namespace Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> RetrieveAllAsync();
        Task<Product?> RetrieveAsync(int id);
        Task<Product?> CreateAsync(Product data);
        Task<Product?> UpdateAsync(int id, Product data);
        Task<bool?> DeleteAsync(int id);
    }
}

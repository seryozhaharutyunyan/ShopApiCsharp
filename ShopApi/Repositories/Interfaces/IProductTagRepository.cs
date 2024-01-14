using Models;

namespace Repositories.Interfaces
{
    public interface IProductTagRepository
    {
        Task<IEnumerable<ProductTag>> RetrieveAllAsync();
        Task<ProductTag?> RetrieveAsync(int id);
        Task<ProductTag?> CreateAsync(ProductTag data);
        Task<ProductTag?> UpdateAsync(int id, ProductTag data);
        Task<bool?> DeleteAsync(int id);
    }
}

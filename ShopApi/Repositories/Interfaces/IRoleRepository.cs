using Models;

namespace Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> RetrieveAllAsync();
        Task<Role?> RetrieveAsync(int id);
        Task<Role?> CreateAsync(Role data);
        Task<Role?> UpdateAsync(int id, Role data);
        Task<bool?> DeleteAsync(int id);
    }
}

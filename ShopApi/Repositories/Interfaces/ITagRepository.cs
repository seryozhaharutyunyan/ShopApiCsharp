using Models;

namespace Repositories.Interfaces
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> RetrieveAllAsync();
        Task<Tag?> RetrieveAsync(int id);
        Task<Tag?> CreateAsync(Tag data);
        Task<Tag?> UpdateAsync(int id, Tag data);
        Task<bool?> DeleteAsync(int id);
    }
}

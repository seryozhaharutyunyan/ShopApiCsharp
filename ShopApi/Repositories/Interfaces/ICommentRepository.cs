using Models;

namespace Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> RetrieveAllAsync();
        Task<Comment?> RetrieveAsync(int id);
        Task<Comment?> CreateAsync(Comment data);
        Task<Comment?> UpdateAsync(int id, Comment data);
        Task<bool?> DeleteAsync(int id);
    }
}

using Models;

namespace Repositories.Interfaces
{
    public interface ILikeRepository
    {
        Like? RetrieveAsync(Like data);
        Task<bool> Attach(Like data);
    }
}

using Models;

namespace Repositories.Interfaces
{
    public interface IUserTokenRepository
    {
        UserToken? Retrieve(UserToken data);
        Task<UserToken?> CreateAsync(UserToken data);
        Task<UserToken?> UpdateAsync(UserToken data);
        Task<bool?> DeleteAsync(UserToken data);
    }
}

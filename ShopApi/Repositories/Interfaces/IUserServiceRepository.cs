using Models;

namespace Repositories.Interfaces
{
    public interface IUserServiceRepository
    {
        Task<bool> IsValidUserAsync(Login user);

        UserToken AddUserRefreshTokens(UserToken user);

        UserToken GetSavedRefreshTokens(string username, string refreshtoken);

        void DeleteUserRefreshTokens(string username, string refreshToken);
    }
}

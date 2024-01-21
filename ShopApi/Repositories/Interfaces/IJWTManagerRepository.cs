using Auth;
using Models;

namespace Repositories.Interfaces
{
    public interface IJWTManagerRepository
    {
        Tokens GenerateToken(User data);
        Tokens GenerateRefreshToken(User data);
    }
}

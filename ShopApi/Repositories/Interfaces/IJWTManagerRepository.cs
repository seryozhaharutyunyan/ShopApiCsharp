using Tokens;
using System.Security.Claims;

namespace Repositories.Interfaces
{
    public interface IJWTManagerRepository
    {
        Token? GenerateToken(string userEmail);
        Token? GenerateRefreshToken(string userEmail);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}

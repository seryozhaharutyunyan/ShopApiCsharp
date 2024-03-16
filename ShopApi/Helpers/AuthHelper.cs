using Auth;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Helpers
{
    public static class AuthHelper
    {
        public static string GenerateHash(string password, string slat = "")
        {
            using (SHA512 sha = SHA512.Create())
            {
                byte[] pHash = sha.ComputeHash(
                    Encoding.Unicode.GetBytes(password)
                    );
                
                byte[] sHash = sha.ComputeHash(
                    Encoding.Unicode.GetBytes(slat)
                    );

                string passwordHash = Encoding.Unicode.GetString(sHash) +
                    Encoding.Unicode.GetString(pHash);

                byte[] passwordData = sha.ComputeHash(
                    Encoding.Unicode.GetBytes(passwordHash)
                    );

                return Convert.ToBase64String(passwordData);
            }
        }

        public static bool ValidatePassword(string password, string passwordHash, string slat = "")
        {
            password = GenerateHash(password, slat);

            return password == passwordHash;
        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public static Tokens GenerateJWTTokens(User data,
            string key,
            string issuer,
            string audience,
            int limit)
        {
            List<Claim> claims = new()
                {
                    new Claim("Id", Convert.ToString(data.UserId)),
                    new Claim(ClaimTypes.Email, data.Email),
                    new Claim(ClaimTypes.Role, data.Role.Name)
                };

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(key));
            SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            DateTime now = DateTime.UtcNow;

            JwtSecurityToken jwt = new(
                    issuer: issuer,
                    audience: audience,
                    notBefore: now,
                    claims: claims,
                    expires: now.AddMinutes(Convert.ToInt32(limit)),
                    signingCredentials: credentials
                    );
            string tokenString = new JwtSecurityTokenHandler().WriteToken(jwt);
            string refreshToken = GenerateRefreshToken();

            return new Tokens { Access_Token = tokenString, Refresh_Token = refreshToken };
        }

        public static IEnumerable<Claim>? TokenDecode(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            SecurityToken? securityToken = tokenHandler.ReadToken(token);
            JwtSecurityToken? jwtSecurityToken = securityToken as JwtSecurityToken;

            return jwtSecurityToken?.Claims;
        }
    }
}

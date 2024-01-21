using Auth;
using Helpers;
using Models;
using Repositories.Interfaces;


namespace Repositories
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly IConfiguration configuration;

        public JWTManagerRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public Tokens GenerateToken(User data)
        {
            return AuthHelper.GenerateJWTTokens(data,
                configuration["JWT:Key"],
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                Convert.ToInt32(configuration["Jwt:Limit"])
                );
        }

        public Tokens GenerateRefreshToken(User data)
        {
            return AuthHelper.GenerateJWTTokens(data,
                configuration["JWT:Key"],
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                Convert.ToInt32(configuration["Jwt:Limit"])
                );
        }

    }
}

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using Repositories.Interfaces;

namespace Repositories
{
    public class UserTokenRepository : IUserTokenRepository
    {
        //private readonly UserManager<IdentityUser> _userManager;
        private readonly ShopDb db;

        public UserTokenRepository(ShopDb db)
        {
            //_userManager = userManager;
            this.db = db;
        }

        public UserToken? RetrieveIsInvalid(UserToken data)
        {
            UserToken? userToken = db.UserTokens.FirstOrDefault(x =>
            x.UserEmail == data.UserEmail &&
            x.IsActive == data.IsActive);

            return userToken;
        }
        
        public UserToken? Retrieve(UserToken data)
        {
            UserToken? userToken = db.UserTokens.FirstOrDefault(x =>
            x.UserEmail == data.UserEmail &&
            x.AccessToken == data.AccessToken &&
            x.RefreshToken == data.RefreshToken &&
            x.IsActive == data.IsActive);

            return userToken;
        }

        public async Task<UserToken?> CreateAsync(UserToken data)
        {
            UserToken userToken = (await db.UserTokens.AddAsync(data)).Entity;
            return await db.SaveChangesAsync() == 1 ? userToken : null;
        }

        public async Task<bool?> DeleteAsync(UserToken data)
        {
            if (data is null)
            {
                return null;
            }

            db.Remove(data);

            return await db.SaveChangesAsync() == 1;

        }

        public async Task<UserToken?> UpdateAsync(UserToken data)
        {
            if (data is null)
            {
                return null;
            }

            db.Update(data);
            return await db.SaveChangesAsync() == 1 ? data : null;
        }
    }
}

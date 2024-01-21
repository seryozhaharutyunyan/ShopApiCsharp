using Models;
using Repositories.Interfaces;

namespace Repositories
{
    public class UserTokenRepository : IUserTokenRepository
    {
        //private readonly UserManager<IdentityUser> _userManager;
        private readonly ShopDb db;

        public UserTokenRepository( ShopDb db)
        {
            //_userManager = userManager;
            this.db = db;
        }

        public UserToken? Retrieve(UserToken data)
        {
            UserToken? userToken = db.UserTokens.FirstOrDefault(x=> 
            x.UserEmail == data.UserEmail &&
            x.Token == data.Token &&
            x.IsActive == true);
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
            return await db.SaveChangesAsync() == 1 ? data: null;
        }
    }
}

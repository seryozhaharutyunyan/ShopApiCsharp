using Microsoft.AspNetCore.Identity;
using Models;
using System;

namespace Repositories
{
    public class UserServiceRepository
    {
        private readonly ShopDb db;

        public UserServiceRepository(UserManager<IdentityUser> userManager, ShopDb db)
        {
            this.db = db;
        }

        public UserToken AddUserRefreshTokens(UserToken data)
        {
            db.UserTokens.Add(data);
            db.SaveChanges();
            return data;
        }

        public void DeleteUserRefreshTokens(string userEmail, string refreshToken)
        {
            var item = db.UserTokens.FirstOrDefault(x => x.UserEmail == userEmail && x.RefreshToken == refreshToken);
            if (item != null)
            {
                db.UserTokens.Remove(item);
            }
        }

        public UserToken GetSavedRefreshTokens(string userEmail, string refreshToken)
        {
            return db.UserTokens.FirstOrDefault(x => x.UserEmail == userEmail && x.RefreshToken == refreshToken && x.IsActive == true);
        }

        public async Task<bool> IsValidUserAsync(Login data)
        {
            var u = db.Users.FirstOrDefault(o => o.Email == data.Email && o.Password == data.Password);

            if (u != null)
            {
                return true;
            }

            return false;
        }
    }
}

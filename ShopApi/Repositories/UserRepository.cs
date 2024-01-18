using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using Repositories.Interfaces;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ShopDb db;

        public UserRepository(ShopDb db)
        {
            this.db = db;
        }

        public async Task<User?> CreateAsync(User data)
        {
            User user = (await db.Users.AddAsync(data)).Entity;
            return await db.SaveChangesAsync() == 1 ? user : null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            User? user = db.Users.Find(id);

            db.Users.Remove(user);
            return await db.SaveChangesAsync() == 1 ? true : false;
        }

        public async Task<IEnumerable<User>> RetrieveAllAsync()
        {
            IEnumerable<User> users = db.Users.ToList();
            return await Task.FromResult<IEnumerable<User>>(users);
        }

        public async Task<User?> RetrieveAsync(int id)
        {
            User? user = await db.Users.FindAsync(id);
            return user is null ? null : user;
        }

        public async Task<User?> UpdateAsync(int id, User data)
        {
            try
            {         
                data.UserId = id;
                User user = (db.Users.Update(data)).Entity;

                return await db.SaveChangesAsync() == 1 ? user : null;
            }  
            catch
            {
                return null;
            } 
        }
    }
}

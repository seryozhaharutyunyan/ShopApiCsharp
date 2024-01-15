using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using Repositories.Interfaces;

namespace Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ShopDb db;

        public RoleRepository(ShopDb db)
        {
            this.db = db;
        }

        public async Task<Role?> CreateAsync(Role data)
        {
            Role role = (await db.Roles.AddAsync(data)).Entity;
            return await db.SaveChangesAsync() == 1 ? role : null;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            Role? role = db.Roles.Find(id);

            if (role is null)
            {
                return null;
            }

            db.Roles.Remove(role);
            return await db.SaveChangesAsync() == 1 ? true : false;
        }

        public async Task<IEnumerable<Role>> RetrieveAllAsync()
        {
            IEnumerable<Role> roles = db.Roles.ToList();
            return await Task.FromResult<IEnumerable<Role>>(roles);
        }

        public async Task<Role?> RetrieveAsync(int id)
        {
            Role? role = await db.Roles.FindAsync(id);
            return role is null ? null : role;
        }

        public async Task<Role?> UpdateAsync(int id, Role data)
        {
            try
            {         
                data.RoleId = id;
                Role role = (db.Roles.Update(data)).Entity;

                return await db.SaveChangesAsync() == 1 ? role : null;
            }  
            catch
            {
                return null;
            } 
        }
    }
}

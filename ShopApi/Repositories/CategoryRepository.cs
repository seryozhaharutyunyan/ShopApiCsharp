using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using Repositories.Interfaces;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ShopDb db;

        public CategoryRepository(ShopDb db)
        {
            this.db = db;
        }

        public async Task<Category?> CreateAsync(Category data)
        {
            Category category = (await db.Categories.AddAsync(data)).Entity;
            return await db.SaveChangesAsync() == 1 ? category : null;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            Category? category = db.Categories.Find(id);

            if (category is null)
            {
                return null;
            }

            db.Categories.Remove(category);
            return await db.SaveChangesAsync() == 1 ? true : false;
        }

        public async Task<IEnumerable<Category>> RetrieveAllAsync()
        {
            IEnumerable<Category> roles = db.Categories.ToList();
            return await Task.FromResult<IEnumerable<Category>>(roles);
        }

        public async Task<Category?> RetrieveAsync(int id)
        {
            Category? role = await db.Categories.FindAsync(id);
            return role is null ? null : role;
        }

        public async Task<Category?> UpdateAsync(int id, Category data)
        {
            Category? role = db.Categories.Find(id);

            if (role is null)
            {
                return null;
            }

            data.CategoryId = id;
            db.Categories.Update(data);

            return await db.SaveChangesAsync() == 1 ? data : null;
        }
    }
}

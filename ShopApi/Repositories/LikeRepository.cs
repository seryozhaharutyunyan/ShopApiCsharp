using Models;
using Repositories.Interfaces;

namespace Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly ShopDb db;

        public LikeRepository(ShopDb db)
        {
            this.db = db;
        }

        public async Task<Like?> CreateAsync(Like data)
        {
            Like like = (await db.Likes.AddAsync(data)).Entity;
            return await db.SaveChangesAsync() == 1 ? like : null;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            Like? like = db.Likes.Find(id);

            if (like is null)
            {
                return null;
            }

            db.Likes.Remove(like);
            return await db.SaveChangesAsync() == 1;
        }

        public async Task<IEnumerable<Like>> RetrieveAllAsync()
        {
            IEnumerable<Like> likes = db.Likes.ToList();
            return await Task.FromResult<IEnumerable<Like>>(likes);
        }

        public async Task<Like?> RetrieveAsync(int id)
        {
            Like? like = await db.Likes.FindAsync(id);
            return like;
        }

        public async Task<Like?> UpdateAsync(int id, Like data)
        {
            try
            {         
                data.LikeId = id;
                Like like = (db.Likes.Update(data)).Entity;

                return await db.SaveChangesAsync() == 1 ? like : null;
            }  
            catch
            {
                return null;
            } 
        }
    }
}

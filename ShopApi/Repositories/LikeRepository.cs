using Models;
using Repositories.Interfaces;
using System.Net.Mail;

namespace Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly ShopDb db;

        public LikeRepository(ShopDb db)
        {
            this.db = db;
        }

        public async Task<bool> Attach(Like data)
        {
            Like? like = RetrieveAsync(data);

            if (like is null)
            {
                await db.Likes.AddAsync(data);
            }
            else
            {
                db.Likes.Remove(data);
            }
            return await db.SaveChangesAsync() == 1;
        }

        public Like? RetrieveAsync(Like data)
        {
            Like? like = db.Likes.FirstOrDefault(l =>
                l.ProductId == data.ProductId && 
                l.UserId == data.UserId
            );
            return like;
        }

    }
}

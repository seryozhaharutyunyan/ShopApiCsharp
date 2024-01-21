using Models;
using Repositories.Interfaces;

namespace Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ShopDb db;

        public CommentRepository(ShopDb db)
        {
            this.db = db;
        }

        public async Task<Comment?> CreateAsync(Comment data)
        {
            Comment comment = (await db.Comments.AddAsync(data)).Entity;
            return await db.SaveChangesAsync() == 1 ? comment : null;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            Comment? comment = db.Comments.Find(id);

            if (comment is null)
            {
                return null;
            }

            db.Comments.Remove(comment);
            return await db.SaveChangesAsync() == 1;
        }

        public async Task<IEnumerable<Comment>> RetrieveAllAsync()
        {
            IEnumerable<Comment> comments = db.Comments.ToList();
            return await Task.FromResult<IEnumerable<Comment>>(comments);
        }

        public async Task<Comment?> RetrieveAsync(int id)
        {
            Comment? comment = await db.Comments.FindAsync(id);
            return comment;
        }

        public async Task<Comment?> UpdateAsync(int id, Comment data)
        {
            try
            {         
                data.CommentId = id;
                Comment comment = (db.Comments.Update(data)).Entity;

                return await db.SaveChangesAsync() == 1 ? comment : null;
            }  
            catch
            {
                return null;
            } 
        }
    }
}

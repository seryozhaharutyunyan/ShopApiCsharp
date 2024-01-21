using Models;
using Repositories.Interfaces;

namespace Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ShopDb db;

        public TagRepository(ShopDb db)
        {
            this.db = db;
        }

        public async Task<Tag?> CreateAsync(Tag data)
        {
            Tag tag = (await db.Tags.AddAsync(data)).Entity;
            return await db.SaveChangesAsync() == 1 ? tag : null;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            Tag? tag = db.Tags.Find(id);

            if (tag is null)
            {
                return null;
            }

            db.Tags.Remove(tag);
            return await db.SaveChangesAsync() == 1;
        }

        public async Task<IEnumerable<Tag>> RetrieveAllAsync()
        {
            IEnumerable<Tag> tags = db.Tags.ToList();
            return await Task.FromResult<IEnumerable<Tag>>(tags);
        }

        public async Task<Tag?> RetrieveAsync(int id)
        {
            Tag? tag = await db.Tags.FindAsync(id);
            return tag;
        }

        public async Task<Tag?> UpdateAsync(int id, Tag data)
        {
            try
            {         
                data.TagId = id;
                Tag tag = (db.Tags.Update(data)).Entity;

                return await db.SaveChangesAsync() == 1 ? tag : null;
            }  
            catch
            {
                return null;
            } 
        }
    }
}

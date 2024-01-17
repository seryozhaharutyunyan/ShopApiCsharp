using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using Repositories.Interfaces;

namespace Repositories
{
    public class ProductTagRepository : IProductTagRepository
    {
        private readonly ShopDb db;

        public ProductTagRepository(ShopDb db)
        {
            this.db = db;
        }

        public async Task<ProductTag?> CreateAsync(ProductTag data)
        {
            ProductTag productTag = (await db.ProductTags.AddAsync(data)).Entity;
            return await db.SaveChangesAsync() == 1 ? productTag : null;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            ProductTag? productTag = db.ProductTags.Find(id);

            if (productTag is null)
            {
                return null;
            }

            db.ProductTags.Remove(productTag);
            return await db.SaveChangesAsync() == 1 ? true : false;
        }

        public async Task<IEnumerable<ProductTag>> RetrieveAllAsync()
        {
            IEnumerable<ProductTag> productTags = db.ProductTags.ToList();
            return await Task.FromResult<IEnumerable<ProductTag>>(productTags);
        }

        public async Task<ProductTag?> RetrieveAsync(int id)
        {
            ProductTag? productTag = await db.ProductTags.FindAsync(id);
            return productTag is null ? null : productTag;
        }

        public async Task<ProductTag?> UpdateAsync(int id, ProductTag data)
        {
            try
            {         
                data.Id = id;
                ProductTag productTag = (db.ProductTags.Update(data)).Entity;

                return await db.SaveChangesAsync() == 1 ? productTag : null;
            }  
            catch
            {
                return null;
            } 
        }
    }
}

using Models;
using Repositories.Interfaces;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopDb db;

        public ProductRepository(ShopDb db)
        {
            this.db = db;
        }

        public async Task<Product?> CreateAsync(Product data)
        {
            Product product = (await db.Products.AddAsync(data)).Entity;
            return await db.SaveChangesAsync() == 1 ? product : null;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            Product? product = db.Products.Find(id);

            if (product is null)
            {
                return null;
            }

            db.Products.Remove(product);
            return await db.SaveChangesAsync() == 1;
        }

        public async Task<IEnumerable<Product>> RetrieveAllAsync()
        {
            IEnumerable<Product> products = db.Products.ToList();
            return await Task.FromResult<IEnumerable<Product>>(products);
        }

        public async Task<Product?> RetrieveAsync(int id)
        {
            Product? product = await db.Products.FindAsync(id);
            return product;
        }

        public async Task<Product?> UpdateAsync(int id, Product data)
        {
            try
            {         
                data.ProductId = id;
                Product product = (db.Products.Update(data)).Entity;

                return await db.SaveChangesAsync() == 1 ? product : null;
            }  
            catch
            {
                return null;
            } 
        }
    }
}

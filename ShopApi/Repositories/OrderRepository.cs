using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using Repositories.Interfaces;

namespace Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShopDb db;

        public OrderRepository(ShopDb db)
        {
            this.db = db;
        }

        public async Task<Order?> CreateAsync(Order data)
        {
            Order order = (await db.Orders.AddAsync(data)).Entity;
            return await db.SaveChangesAsync() == 1 ? order : null;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            Order? order = db.Orders.Find(id);

            if (order is null)
            {
                return null;
            }

            db.Orders.Remove(order);
            return await db.SaveChangesAsync() == 1 ? true : false;
        }

        public async Task<IEnumerable<Order>> RetrieveAllAsync()
        {
            IEnumerable<Order> orders = db.Orders.ToList();
            return await Task.FromResult<IEnumerable<Order>>(orders);
        }

        public async Task<Order?> RetrieveAsync(int id)
        {
            Order? order = await db.Orders.FindAsync(id);
            return order is null ? null : order;
        }

        public async Task<Order?> UpdateAsync(int id, Order data)
        {
            try
            {         
                data.OrderId = id;
                Order order = (db.Orders.Update(data)).Entity;

                return await db.SaveChangesAsync() == 1 ? order : null;
            }  
            catch
            {
                return null;
            } 
        }
    }
}

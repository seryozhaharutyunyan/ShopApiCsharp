using Models;
using Repositories.Interfaces;

namespace Repositories
{
    public class EdgeRepository : IEdgeRepository
    {
        private readonly ShopDb db;

        public EdgeRepository(ShopDb db)
        {
            this.db = db;
        }

        public async Task<Edge?> CreateAsync(Edge data)
        {
            Edge edge = (await db.Edges.AddAsync(data)).Entity;
            return await db.SaveChangesAsync() == 1 ? edge : null;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            Edge? edge = db.Edges.Find(id);

            if (edge is null)
            {
                return null;
            }

            db.Edges.Remove(edge);
            return await db.SaveChangesAsync() == 1 ? true : false;
        }

        public async Task<IEnumerable<Edge>> RetrieveAllAsync()
        {
            IEnumerable<Edge> edges = db.Edges.ToList();
            return await Task.FromResult<IEnumerable<Edge>>(edges);
        }

        public async Task<Edge?> RetrieveAsync(int id)
        {
            Edge? edge = await db.Edges.FindAsync(id);
            return edge is null ? null : edge;
        }

        public async Task<Edge?> UpdateAsync(int id, Edge data)
        {
            try
            {         
                data.EdgeId = id;
                Edge edge = (db.Edges.Update(data)).Entity;

                return await db.SaveChangesAsync() == 1 ? edge : null;
            }  
            catch
            {
                return null;
            } 
        }
    }
}

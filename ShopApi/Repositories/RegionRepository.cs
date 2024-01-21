using Models;
using Repositories.Interfaces;

namespace Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly ShopDb db;

        public RegionRepository(ShopDb db)
        {
            this.db = db;
        }

        public async Task<Region?> CreateAsync(Region data)
        {
            Region region = (await db.Regions.AddAsync(data)).Entity;
            return await db.SaveChangesAsync() == 1 ? region : null;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            Region? region = db.Regions.Find(id);

            if (region is null)
            {
                return null;
            }

            db.Regions.Remove(region);
            return await db.SaveChangesAsync() == 1;
        }

        public async Task<IEnumerable<Region>> RetrieveAllAsync()
        {
            IEnumerable<Region> regions = db.Regions.ToList();
            return await Task.FromResult<IEnumerable<Region>>(regions);
        }

        public async Task<Region?> RetrieveAsync(int id)
        {
            Region? region = await db.Regions.FindAsync(id);
            return region;
        }

        public async Task<Region?> UpdateAsync(int id, Region data)
        {
            try
            {         
                data.RegionId = id;
                Region region = (db.Regions.Update(data)).Entity;

                return await db.SaveChangesAsync() == 1 ? region : null;
            }  
            catch
            {
                return null;
            } 
        }
    }
}

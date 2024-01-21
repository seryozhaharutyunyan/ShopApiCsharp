using Models;
using Repositories.Interfaces;

namespace Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly ShopDb db;

        public CityRepository(ShopDb db)
        {
            this.db = db;
        }

        public async Task<City?> CreateAsync(City data)
        {
            City City = (await db.Cities.AddAsync(data)).Entity;
            return await db.SaveChangesAsync() == 1 ? City : null;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            City? city = db.Cities.Find(id);

            if (city is null)
            {
                return null;
            }

            db.Cities.Remove(city);
            return await db.SaveChangesAsync() == 1;
        }

        public async Task<IEnumerable<City>> RetrieveAllAsync()
        {
            IEnumerable<City> cities = db.Cities.ToList();
            return await Task.FromResult<IEnumerable<City>>(cities);
        }

        public async Task<City?> RetrieveAsync(int id)
        {
            City? city = await db.Cities.FindAsync(id);
            return city;
        }

        public async Task<City?> UpdateAsync(int id, City data)
        {
            try
            {
                data.CityId = id;
                City city = (db.Cities.Update(data)).Entity;

                return await db.SaveChangesAsync() == 1 ? city : null;
            }
            catch
            {
                return null;
            }
        }
    }
}

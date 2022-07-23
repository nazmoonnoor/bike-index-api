using Microsoft.EntityFrameworkCore;
using Theft.Core;
using Theft.Core.Domains;

namespace Theft.Core.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly ApplicationDbContext context;

        public CityRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<City> CreateAsync(City city, CancellationToken cancellationToken = default)
        {
            if (city == null)
                throw new ArgumentNullException(nameof(city));

            try
            {
                await context.Set<City>().AddAsync(city);
                await context.SaveChangesAsync();

                return city;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<City> UpdateAsync(string id, Func<City, Task<City>> callback, CancellationToken cancellationToken = default)
        {
            var current = await GetByIdAsync(new Guid(id), cancellationToken);

            if (current == null)
                throw new ArgumentException($"City '{id}' is not valid or doesn't exist.", nameof(id));

            try
            {
                var modified = await callback(current);

                if (current != null)
                {
                    context.Entry(current).State = EntityState.Detached;
                }

                context.Entry(modified).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return modified;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<City> DeleteAsync(City city, CancellationToken cancellation = default)
        {
            try
            {
                var entity = context.Set<City>().Remove(city);
                await context.SaveChangesAsync();
                return entity.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IList<City>> GetAllCitiesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await context.Set<City>().Where(b => b.IsActive == true).ToListAsync();

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<City?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException("Identifier is not provided.");

            try
            {
                var response = await context.Set<City>().SingleOrDefaultAsync(e => e.Id == id);

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

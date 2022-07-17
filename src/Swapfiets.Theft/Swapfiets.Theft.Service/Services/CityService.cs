using Swapfiets.Theft.Core.Domains;
using Swapfiets.Theft.Core.Repositories;

namespace Swapfiets.Theft.Service.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            this.cityRepository = cityRepository;
        }
        public async Task<City> CreateAsync(City city, CancellationToken cancellationToken)
        {
            if (city == null)
                throw new ArgumentNullException(nameof(city));

            if (city.Id == Guid.Empty)
                throw new ArgumentException("City identifier is required.", nameof(city));

            if (string.IsNullOrEmpty(city.Name))
                throw new ArgumentNullException("City name is required.");

            if (string.IsNullOrEmpty(city.Country))
                throw new ArgumentNullException("Country name is required.");

            try
            {
                city.IsActive = true;
                city.Created = DateTime.UtcNow;
                var created = await cityRepository.CreateAsync(city, cancellationToken);

                return created;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<City> UpdateAsync(string id, Func<City, Task<City>> callback, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            try
            {
                var updated = await cityRepository.UpdateAsync(id, async (existing) =>
                {
                    var modified = await callback((City)existing.Clone());

                    return modified;
                });

                return updated;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAsync(string id, City city, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            if (city == null)
                throw new ArgumentNullException(nameof(city));

            try
            {
                city.Id = new Guid(id);
                var deleted = await cityRepository.DeleteAsync(city, cancellationToken);

                return deleted;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IList<City>> GetAllCitiesAsync(CancellationToken cancellationToken)
        {
            try
            {
                var cities = await cityRepository.GetAllCitiesAsync(cancellationToken);

                return cities;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<City?> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            try
            {
                var cityId = new Guid(id);
                var cities = await cityRepository.GetByIdAsync(cityId, cancellationToken);

                return cities;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

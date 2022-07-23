using Theft.Core.Domains;
using Theft.Core.Repositories;

namespace Theft.Service.Services
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

        public async Task<City> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            try
            {
                var cityId = new Guid(id);
                var existing = await cityRepository.GetByIdAsync(cityId, cancellationToken);
                if (existing == null)
                    throw new Exception("City not found!");

                var deleted = await cityRepository.DeleteAsync(existing, cancellationToken);

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
                var city = await cityRepository.GetByIdAsync(cityId, cancellationToken);

                return city;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

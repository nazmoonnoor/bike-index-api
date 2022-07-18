using Swapfiets.Theft.Core.Domains;

namespace Swapfiets.Theft.Service.Services
{
    public interface ICityService
    {
        /// <summary>
        /// Creates city
        /// </summary>
        /// <param name="city"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<City> CreateAsync(City city, CancellationToken cancellationToken);

        /// <summary>
        /// Updates city
        /// </summary>
        /// <param name="id"></param>
        /// <param name="city"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<City> UpdateAsync(string id, Func<City, Task<City>> callback, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes city
        /// </summary>
        /// <param name="id"></param>
        /// <param name="city"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<City> DeleteAsync(string id, CancellationToken cancellationToken);

        /// <summary>
        /// Gets by city id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<City?> GetByIdAsync(string id, CancellationToken cancellationToken);

        /// <summary>
        /// Gets all cities
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IList<City>> GetAllCitiesAsync(CancellationToken cancellationToken);
    }
}

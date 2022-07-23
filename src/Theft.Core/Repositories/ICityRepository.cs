using Theft.Core.Domains;

namespace Theft.Core.Repositories
{
    /// <summary>
    /// Represents City repository functions
    /// </summary>
    public interface ICityRepository
    {
        /// <summary>
        /// Creates city 
        /// </summary>
        /// <param name="city"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<City> CreateAsync(City city, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates city
        /// </summary>
        /// <param name="city"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<City> UpdateAsync(string id, Func<City, Task<City>> callback, CancellationToken cancellation = default);

        /// <summary>
        /// Deletes city
        /// </summary>
        /// <param name="city"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<City> DeleteAsync(City city, CancellationToken cancellation = default);

        /// <summary>
        /// Gets city by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<City?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all cities 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IList<City>> GetAllCitiesAsync(CancellationToken cancellationToken = default);
    }
}

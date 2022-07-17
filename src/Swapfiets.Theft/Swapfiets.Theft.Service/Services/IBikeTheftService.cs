using Swapfiets.Theft.Service.Models;

namespace Swapfiets.Theft.Service.Services
{
    /// <summary>
    /// Represents the service to get bike theft related information from different cities
    /// </summary>
    public interface IBikeTheftService
    {
        /// <summary>
        /// Gets thefts data based on filter query
        /// </summary>
        /// <param name="queryParams">Filter criteria</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>BikeTheftResponse</returns>
        Task<BikeTheftResponse> SearchAsync(BikeTheftQueryParams queryParams, CancellationToken cancellationToken);

        /// <summary>
        /// Gets theft count based on filter query
        /// </summary>
        /// <param name="queryParams">Filter criteria</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>BikeTheftCountResponse</returns>
        Task<BikeTheftCountResponse> SearchCountAsync(BikeTheftQueryParams queryParams, CancellationToken cancellationToken);
    }
}

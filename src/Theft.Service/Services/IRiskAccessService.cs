using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theft.Service.Services
{
    /// <summary>
    /// Represents the service to get bike theft assessment among locations
    /// </summary>
    public interface IRiskAssessService
    {
        /// <summary>
        /// Get list of theft count among cities, to assess the risk
        /// </summary>
        /// <param name="cities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Dictionary<string, int>?> RiskAssessByLocationAsync(string? location, CancellationToken cancellationToken);
    }
}

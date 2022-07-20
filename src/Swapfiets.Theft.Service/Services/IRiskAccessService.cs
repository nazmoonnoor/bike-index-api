using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapfiets.Theft.Service.Services
{
    /// <summary>
    /// Represents the service to get bike theft assessment among locations
    /// </summary>
    public interface IRiskAssessService
    {
        /// <summary>
        /// Assess risk of bike theft among cities
        /// </summary>
        /// <param name="cities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Dictionary<string, int>> RiskAssessByCityAsync(string city, CancellationToken cancellationToken);
    }
}

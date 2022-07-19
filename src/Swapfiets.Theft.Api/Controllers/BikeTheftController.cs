using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swapfiets.Theft.Service.Models;
using Swapfiets.Theft.Service.Services;
using System.Net;

namespace Swapfiets.Theft.Api.Controllers
{
    /// <summary>
    /// Represents the bike theft resources
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v1/bike-theft")]
    public class BikeTheftController : BaseController<BikeTheftController>
    {
        private readonly IBikeTheftService bikeTheftService;

        /// <summary>
        /// Injects the dependencies
        /// </summary>
        /// <param name="bikeTheftService">Initializes the bike theft service</param>
        public BikeTheftController(IBikeTheftService bikeTheftService)
        {
            this.bikeTheftService = bikeTheftService;
        }

        /// <summary>
        /// Gets count of theft bikes
        /// </summary>
        /// <param name="city">(Optional) City name</param>
        /// <param name="latlng">(Optioinal) Latlng of the location</param>
        /// <param name="distance">(Optioinal) Distance: default value is 10</param>
        /// <returns></returns>
        [HttpGet]
        [Route("count")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BikeTheftCountResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetBikeTheftCount(string? city, string? latlng, int distance)
        {
            var query = new BikeTheftQueryParams(city, latlng, distance);

            var response = await bikeTheftService.SearchCountAsync(query, HttpContext.RequestAborted);

            if (response == null)
                return BadRequest();

            return Ok(response);
        }

        /// <summary>
        /// Gets filtered list of theft bikes
        /// </summary>
        /// <param name="city">(Optional) City name</param>
        /// <param name="latlng">(Optioinal) Latlng of the location</param>
        /// <param name="distance">(Optioinal) Distance: default value is 10</param>
        /// <param name="pageSize">(Optioinal) PageSize: default value is 20</param>
        /// <param name="pageNumber">(Optioinal) PageNumber: default value is 1</param>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BikeTheftResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetBikeThefts(string? city, string? latlng, int distance, int pageSize, int pageNumber)
        {
            var query = new BikeTheftQueryParams(city, latlng, distance, pageSize, pageNumber);

            var response = await bikeTheftService.SearchAsync(query, HttpContext.RequestAborted);

            if (response == null)
                return BadRequest();

            return Ok(response);
        }
    }
}

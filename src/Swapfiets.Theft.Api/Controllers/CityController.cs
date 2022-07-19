using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swapfiets.Theft.Service.Services;
using System.Net;

namespace Swapfiets.Theft.Api.Controllers
{
    /// <summary>
    /// Represents the city resources
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v1/city")]
    public class CityController : BaseController<CityController>
    {
        private readonly ICityService cityService;
        private readonly IMapper mapper;

        /// <summary>
        /// Injects the dependencies
        /// </summary>
        /// <param name="cityService">Initializes the city service</param>
        /// <param name="mapper">Initializes the mapper service</param>
        public CityController(ICityService cityService, IMapper mapper)
        {
            this.cityService = cityService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Gets all cities
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var cities = await cityService.GetAllCitiesAsync(HttpContext.RequestAborted);

            if (cities is null)
                return NotFound();

            return Ok(mapper.Map<List<Service.Models.City>>(cities));
        }

        /// <summary>
        /// Gets city by identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get(string id)
        {
            var city = await cityService.GetByIdAsync(id, HttpContext.RequestAborted);

            if (city is null)
                return NotFound();

            return Ok(mapper.Map<Service.Models.City>(city));
        }

        /// <summary>
        /// Creates a new city
        /// </summary>
        /// <param name="newCity"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Post(Service.Models.City newCity)
        {
            var city = mapper.Map<Core.Domains.City>(newCity);

            // Generate unique city identifier
            city.Id = Guid.NewGuid();

            var created = await cityService.CreateAsync(city, HttpContext.RequestAborted);
            
            if (created is null)
                return NotFound();

            return Ok(mapper.Map<Service.Models.City>(created));
        }

        /// <summary>
        /// Updates city
        /// </summary>
        /// <param name="id">Required. City identifier.</param>
        /// <param name="city"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateAsync(string id, Service.Models.City city)
        {
            var changed = mapper.Map<Core.Domains.City>(city);
            var updated = await cityService.UpdateAsync(id, existing =>
            {
                existing.Id = new Guid(id);
                existing.Name = changed.Name;
                existing.Country = changed.Country;
                existing.Created = changed.Created;
                existing.CreatedBy = changed.CreatedBy;
                return Task.FromResult(existing);
            }, HttpContext.RequestAborted);

            if (updated == null)
                return NotFound();

            return Ok(mapper.Map<Service.Models.City>(updated));
        }

        /// <summary>
        /// Deletes city
        /// </summary>
        /// <param name="id">Required. City identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteAsync(string id) 
        {
            var deleted = await cityService.DeleteAsync(id, HttpContext.RequestAborted);
            
            if (deleted == null)
                return NotFound();

            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Theft.Service.Models;
using System.Net;
using Theft.Service.Services;

namespace Theft.Api.Controllers
{
    /// <summary>
    /// Represents the risk assess resources
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v1/risk-assess")]
    public class RiskAssessController : BaseController<RiskAssessController>
    {
        private readonly IRiskAssessService riskAssessService;

        /// <summary>
        /// Injects the dependencies
        /// </summary>
        /// <param name="riskAssessService">Initializes the bike theft service</param>
        public RiskAssessController(IRiskAssessService riskAssessService)
        {
            this.riskAssessService = riskAssessService;
        }

        /// <summary>
        /// Assess risk by city
        /// </summary>
        /// <param name="location">(Required) City or lat/lng of the location</param>
        /// <returns></returns>
        [HttpGet]
        [Route("city")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Dictionary<string, int>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetBikeTheftCount(string? location)
        {
            var response = await riskAssessService.RiskAssessByLocationAsync(location, HttpContext.RequestAborted);

            if (response == null)
                return BadRequest();

            return Ok(response);
        }
    }
}

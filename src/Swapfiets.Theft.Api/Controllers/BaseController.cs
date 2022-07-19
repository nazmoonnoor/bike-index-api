using Microsoft.AspNetCore.Mvc;

namespace Swapfiets.Theft.Api.Controllers
{
    /// <summary>
    /// Represents the Base controller of theft api
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseController<T> : ControllerBase where T : BaseController<T>
    {
        private ILogger<T>? _logger;

        /// <summary>
        /// Logger object
        /// </summary>
        protected ILogger<T>? Logger => _logger ?? (_logger = HttpContext.RequestServices.GetService<ILogger<T>>());
    }
}

using Theft.Core.Repositories;
using Theft.Service.Services;

namespace Theft.Api.Configuration
{
    /// <summary>
    /// Configures dependencies
    /// </summary>
    public static class DependencyConfig
    {
        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureDependencies(this IServiceCollection services)
        {
            //
            // Service layer
            services.AddScoped<IBikeTheftService, BikeTheftService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IRiskAssessService, RiskAssessService>();

            //
            //  Storage layer
            services.AddScoped<ICityRepository, CityRepository>();
        }
    }
}

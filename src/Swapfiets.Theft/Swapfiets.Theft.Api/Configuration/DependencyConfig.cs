using Swapfiets.Theft.Core.Repositories;
using Swapfiets.Theft.Service.Services;

namespace Swapfiets.Theft.Api.Configuration
{
    public static class DependencyConfig
    {
        public static void ConfigureDependencies(this IServiceCollection services)
        {
            //
            // Service layer
            services.AddScoped<IBikeTheftService, BikeTheftService>();
            services.AddScoped<ICityService, CityService>();

            //
            //  Storage layer
            services.AddScoped<ICityRepository, CityRepository>();
        }
    }
}

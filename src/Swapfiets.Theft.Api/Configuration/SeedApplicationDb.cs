using Swapfiets.Theft.Core;

namespace Swapfiets.Theft.Api.Configuration
{
    /// <summary>
    /// Seeds application database
    /// </summary>
    public static class SeedApplicationDb
    {
        /// <summary>
        /// Seed data
        /// </summary>
        /// <param name="services"></param>
        public static void SeedData(this IServiceCollection services)
        {
            var dbContext = services.BuildServiceProvider().GetService<ApplicationDbContext>();

            if (dbContext != null)
                ApplicationDbContextSeed.SeedAsync(dbContext);

        }
    }
}

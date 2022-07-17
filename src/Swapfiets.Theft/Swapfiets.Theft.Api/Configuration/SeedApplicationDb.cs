using Swapfiets.Theft.Core;

namespace Swapfiets.Theft.Api.Configuration
{
    public static class SeedApplicationDb
    {
        public static void SeedData(this IServiceCollection services)
        {
            var dbContext = services.BuildServiceProvider().GetService<ApplicationDbContext>();

            if (dbContext != null)
                ApplicationDbContextSeed.SeedAsync(dbContext);

        }
    }
}

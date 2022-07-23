using Theft.Core.Domains;

namespace Theft.Core
{
    /// <summary>
    /// Seeds application database
    /// </summary>
    public class ApplicationDbContextSeed
    {
        /// <summary>
        /// Configure seed data
        /// </summary>
        /// <param name="context"></param>
        public static void SeedAsync(ApplicationDbContext context)
        {
            if (!context.Cities.Any())
            {
                var cities = new List<City>
                {
                    new City
                    {
                        Name = "Amsterdam",
                        Country = "The Netherlands",
                        IsActive = true,
                        Created = DateTime.UtcNow
                    },
                    new City
                    {
                        Name = "Berlin",
                        Country = "Germany",
                        IsActive = true,
                        Created = DateTime.UtcNow
                    },
                    new City
                    {
                        Name = "Copenhagen",
                        Country = "Denmark",
                        IsActive = true,
                        Created = DateTime.UtcNow
                    },
                    new City
                    {
                        Name = "Brussels",
                        Country = "Belgium",
                        IsActive = true,
                        Created = DateTime.UtcNow
                    }
                };

                context.Cities.AddRange(cities);
                context.SaveChanges();
            }
        }
    }
}

using Swapfiets.Theft.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapfiets.Theft.Core
{
    public class ApplicationDbContextSeed
    {
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

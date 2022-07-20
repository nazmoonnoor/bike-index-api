using Microsoft.Extensions.Caching.Memory;

namespace Swapfiets.Theft.Service.Services
{
    public class RiskAssessService : IRiskAssessService
    {
        private const string CacheKey = "LocationTheftCount";
        private readonly IMemoryCache memoryCache;
        private readonly ICityService cityService;
        private readonly IBikeTheftService bikeTheftService;
        private MemoryCacheEntryOptions memoryCacheOption { get { return new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromDays(1))
                    .SetAbsoluteExpiration(TimeSpan.FromDays(7))
                    .SetPriority(CacheItemPriority.Normal)
                    .SetSize(1024);
            } }

        public RiskAssessService(IMemoryCache memoryCache, ICityService cityService, IBikeTheftService bikeTheftService)
        {
            this.memoryCache = memoryCache;
            this.cityService = cityService;
            this.bikeTheftService = bikeTheftService;
        }

        public async Task<Dictionary<string, int>?> RiskAssessByLocationAsync(string? location, CancellationToken cancellationToken)
        {
            if(string.IsNullOrEmpty(location))
                throw new ArgumentNullException(nameof(location));

            Dictionary<string, int>? theftLocations = null;

            // Get all cities
            var cities = await cityService.GetAllCitiesAsync(cancellationToken);

            // Set the user request location
            Func<string, Dictionary<string, int>, Task> setRequestedLocation = async (location, theftCities) =>
            {
                if (!theftCities.ContainsKey(location))
                {
                    var requestedLocation = await bikeTheftService.SearchCountAsync(new Models.BikeTheftQueryParams(location, null, 0), cancellationToken);
                    theftCities.Add(location, requestedLocation.Count);
                }
            };

            // Check if results are cached
            if (memoryCache.TryGetValue(CacheKey, out theftLocations))
            {
                if (theftLocations != null)
                    await setRequestedLocation(location, theftLocations);
            }
            else
            {
                theftLocations = new Dictionary<string, int>();
                foreach (var item in cities)
                {
                    if (!string.IsNullOrEmpty(item.Name) && (item.Name != null && !theftLocations.ContainsKey(item.Name)))
                    {
                        var response = await bikeTheftService.SearchCountAsync(new Models.BikeTheftQueryParams(item.Name, null, 0), cancellationToken);
                        theftLocations.Add(item.Name, response.Count);
                    }
                }

                await setRequestedLocation(location, theftLocations);

                memoryCache.Set(CacheKey, theftLocations, memoryCacheOption);
            }

            return theftLocations;
        }
    }
}

namespace Swapfiets.Theft.Service.Services
{
    public class RiskAssessService : IRiskAssessService
    {
        private readonly ICityService cityService;
        private readonly IBikeTheftService bikeTheftService;

        public RiskAssessService(ICityService cityService, IBikeTheftService bikeTheftService)
        {
            this.cityService = cityService;
            this.bikeTheftService = bikeTheftService;
        }

        public async Task<Dictionary<string, int>> RiskAssessByCityAsync(string city, CancellationToken cancellationToken)
        {
            Dictionary<string, int> theftCounts = new Dictionary<string, int>();
            var cities = await cityService.GetAllCitiesAsync(cancellationToken);

            var requestedCityCount = await bikeTheftService.SearchCountAsync(new Models.BikeTheftQueryParams(city, null, 0), cancellationToken);
            theftCounts.Add(city, requestedCityCount.Count);

            foreach (var item in cities)
            {
                if (!string.IsNullOrEmpty(item.Name) && (item.Name != null && !theftCounts.ContainsKey(item.Name)))
                {
                    var response = await bikeTheftService.SearchCountAsync(new Models.BikeTheftQueryParams(item.Name, null, 0), cancellationToken);
                    theftCounts.Add(item.Name, response.Count);
                }
            }

            return theftCounts;
        }
    }
}

using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Polly;
using Polly.Retry;
using Swapfiets.Theft.Service.Models;
using System.Net;

namespace Swapfiets.Theft.Service.Services
{
    public class BikeTheftService : IBikeTheftService
    {
        private const string BIKE_CLIENT = "BikeHttpClient";
        private readonly IConfiguration configuration;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly int maxRetries;
        private readonly AsyncRetryPolicy retryPolicy;

        public BikeTheftService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            this.configuration = configuration;
            this.httpClientFactory = httpClientFactory;

            maxRetries = Convert.ToInt32(this.configuration.GetSection("MaxRetries").Value);
            retryPolicy = Policy.Handle<HttpRequestException>().RetryAsync(maxRetries);
        }

        public async Task<BikeTheftResponse> SearchAsync(BikeTheftQueryParams queryParams, CancellationToken cancellationToken)
        {
            if (queryParams == null)
                throw new ArgumentNullException(nameof(queryParams));

            if (!queryParams.IsValid())
                throw new ArgumentException("City or lat/long must be provided to get bike theft data!");

            string? location = null;
            if (!string.IsNullOrEmpty(queryParams.City))
                location = $"{queryParams.City}";
            else if (queryParams.GeoCoordinate != null && queryParams.GeoCoordinate.IsValid())
                location = $"{queryParams.GeoCoordinate.Latitude},{queryParams.GeoCoordinate.Longitude}";
            else throw new ArgumentException("City or GeoCoordinate are invalid or not provided.");

            var client = this.httpClientFactory.CreateClient(BIKE_CLIENT);

            return await retryPolicy.ExecuteAsync(async () =>
            {
                var result = await client.GetAsync($"{client.BaseAddress}/api/v3/search/" +
                    $"?page={queryParams.PageNumber}&per_page={queryParams.PageSize}" +
                    $"&location={location}&distance={queryParams.Distance}&stolenness=proximity");

                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    return new BikeTheftResponse(null);
                }

                var resultString = await result.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<BikeTheftResponse>(resultString) ?? new BikeTheftResponse(null);
            });
        }

        public async Task<BikeTheftCountResponse> SearchCountAsync(BikeTheftQueryParams queryParams, CancellationToken cancellationToken)
        {
            if (queryParams == null)
                throw new ArgumentNullException(nameof(queryParams));

            if (!queryParams.IsValid())
                throw new ArgumentException("City or lat/long must be provided to get theft data!");

            string? location = null;
            if (queryParams.City != null)
                location = $"{queryParams.City}";
            else if (queryParams.GeoCoordinate != null && queryParams.GeoCoordinate.IsValid())
                location = $"{queryParams.GeoCoordinate.Latitude},{queryParams.GeoCoordinate.Longitude}";
            else throw new ArgumentNullException("City or GeoCoordinate are invalid or not provided.");

            var client = this.httpClientFactory.CreateClient(BIKE_CLIENT);

            return await retryPolicy.ExecuteAsync(async () =>
            {
                var result = await client.GetAsync($"{client.BaseAddress}/api/v3/search/count/" +
                    $"?location={location}&distance={queryParams.Distance}&stolenness=proximity");

                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    return new BikeTheftCountResponse(0, "", (int)result.StatusCode);
                }

                var resultString = await result.Content.ReadAsStringAsync();

                JObject? response = (JObject?)JsonConvert.DeserializeObject(resultString);
                int count = response == null ? 0 : response.Value<int>("proximity");

                if (count == 0)
                    throw new ArgumentException("Please check if the city or geocoordinate are correct, or increase the distance.");

                return new BikeTheftCountResponse(count, "", (int)result.StatusCode);
            });
        }
    }
}

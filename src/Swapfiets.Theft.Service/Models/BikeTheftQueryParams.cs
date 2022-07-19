namespace Swapfiets.Theft.Service.Models
{
    /// <summary>
    /// Filter query parameters
    /// </summary>
    /// <param name="City">City name</param>
    /// <param name="Location">City location</param>
    /// <param name="Distance">Distance</param>
    public class BikeTheftQueryParams
    {
        public BikeTheftQueryParams(string? city, GeoCoordinate? location, int distance, int pageSize = 20, int pageNumber = 1)
        {
            City = city;
            GeoCoordinate = location;
            Distance = distance != 0 ? distance : 10;
            PageSize = pageSize != 0 ? pageSize : 20;
            PageNumber = pageNumber != 0 ? pageNumber : 1;
        }

        public string? City { get; set; }
        public GeoCoordinate? GeoCoordinate { get; set; }
        public int Distance { get; init; }
        public int PageSize { get; init; }
        public int PageNumber { get; init; }

        /// <summary>
        /// Validate filter criteria
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            if (!string.IsNullOrEmpty(City))
                return true;
            if(GeoCoordinate != null && GeoCoordinate.IsValid())
                return true;

            return false;
        }
    }
}
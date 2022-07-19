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
        public BikeTheftQueryParams(string? city, string? location, int distance, int pageSize = 20, int pageNumber = 1)
        {
            City = city;
            GeoCoordinate = location !=null ? GetGeoCoordinate(location) : null;
            Distance = distance != 0 ? distance : Distance;
            PageSize = pageSize != 0 ? pageSize : 20;
            PageNumber = pageNumber != 0 ? pageNumber : 1;
        }

        public string? City { get; set; }
        public GeoCoordinate? GeoCoordinate { get; set; }
        public int Distance { get; init; } = 10;
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

        /// <summary>
        /// Get GeoCoordinate object based on user input
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public GeoCoordinate? GetGeoCoordinate(string location)
        {
            if (string.IsNullOrEmpty(location))
                return null;

            var latlng = location.Split(',');
            double latitude = 0d;
            double longitude = 0d;
            if (double.TryParse(latlng[0], out latitude)
                && double.TryParse(latlng[1], out longitude))
            {
                var coordinate = new GeoCoordinate(latitude, longitude);

                if (!coordinate.IsValid())
                    throw new Exception("Provide a valid latitude/longitude.");
                
                return coordinate;
            }

            return null;
       }
    }
}
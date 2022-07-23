namespace Theft.Service.Models
{
    /// <summary>
    /// Represents the GeoCoordinate
    /// </summary>
    /// <param name="Latitude">Latitude</param>
    /// <param name="Longitude">Longitude</param>
    public class GeoCoordinate
    {
        public double? Latitude { get; }
        public double? Longitude { get; }
        public GeoCoordinate(double? latitude, double? longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        /// <summary>
        /// Validates geocoordinate
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public bool IsValid()
        {
            if (Latitude == null || Longitude == null)
                return false;

            if (Latitude < -90 || Latitude > 90)
            {
                throw new ArgumentOutOfRangeException("Latitude must be between -90 and 90 degrees inclusive.");
            }

            if (Longitude < -180 || Longitude > 180)
            {
                throw new ArgumentOutOfRangeException("Longitude must be between -180 and 180 degrees inclusive.");
            }

            return true;
        }
    }
}
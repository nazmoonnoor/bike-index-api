namespace Theft.Core.Domains
{
    public class City : EntityBase, ICloneable
    {
        public City()
        {
        }
        public City(City city)
        {
            Name = city.Name;
            Country = city.Country;
            IsActive = city.IsActive;
        }

        /// <summary>
        /// Name of the city
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Country name
        /// </summary>
        public string? Country { get; set; }

        /// <summary>
        /// Is active
        /// </summary>
        public bool IsActive { get; set; }

        public object Clone()
        {
            return new City(this);
        }
    }
}
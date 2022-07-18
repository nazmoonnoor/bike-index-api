namespace Swapfiets.Theft.Service.Models
{
    public class City
    {
        /// <summary>
        /// City identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Created date
        /// </summary>
        public DateTime? Created { get; set; }
        
        /// <summary>
        /// Created by
        /// </summary>
        public DateTime? CreatedBy { get; set; }

        /// <summary>
        /// Name of the city
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Country name
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Is active
        /// </summary>
        public bool IsActive { get; set; }
    }
}

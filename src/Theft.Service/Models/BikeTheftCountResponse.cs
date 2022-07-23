using System.Text.Json.Serialization;

namespace Theft.Service.Models
{
    /// <summary>
    /// Represents the count of bike theft
    /// </summary>
    public record BikeTheftCountResponse(
            int Count,
            string Message,
            int StatusCode
        );
}


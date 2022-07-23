using System.Text.Json.Serialization;

namespace Theft.Service.Models
{
    /// <summary>
    /// Represents bike
    /// </summary>
    public record Bike(
            [property: JsonPropertyName("description")] object Description,
            [property: JsonPropertyName("frame_colors")] IReadOnlyList<string> FrameColors,
            [property: JsonPropertyName("frame_model")] string FrameModel,
            [property: JsonPropertyName("id")] int Id,
            [property: JsonPropertyName("location_found")] object LocationFound,
            [property: JsonPropertyName("serial")] string Serial,
            [property: JsonPropertyName("status")] string Status,
            [property: JsonPropertyName("stolen")] bool Stolen,
            [property: JsonPropertyName("stolen_coordinates")] IReadOnlyList<double> StolenCoordinates,
            [property: JsonPropertyName("stolen_location")] string StolenLocation,
            [property: JsonPropertyName("thumb")] object Thumb,
            [property: JsonPropertyName("title")] string Title,
            [property: JsonPropertyName("url")] string Url
        );

    /// <summary>
    /// Represents bike theft response
    /// </summary>
    /// <param name="Bikes"></param>
    public record BikeTheftResponse(
        [property: JsonPropertyName("bikes")] IEnumerable<Bike>? Bikes
    );
}

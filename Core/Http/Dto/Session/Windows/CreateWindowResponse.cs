using System.Text.Json.Serialization;

namespace Core.Http.Dto.Session.Windows;

public record CreateWindowResponse
{
    [JsonPropertyName("handle")]
    public string Handle { get; init; }
    
    [JsonPropertyName("type")]
    public string Type { get; init; }
}
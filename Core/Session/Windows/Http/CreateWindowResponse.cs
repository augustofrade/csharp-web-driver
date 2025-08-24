using System.Text.Json.Serialization;

namespace Core.Session.Windows.Http;

public record CreateWindowResponse
{
    [JsonPropertyName("handle")]
    public string Handle { get; init; }
    
    [JsonPropertyName("type")]
    public string Type { get; init; }
}
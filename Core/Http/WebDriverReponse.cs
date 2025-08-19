using System.Text.Json.Serialization;

namespace Core.Http;

public record WebDriverReponse<T>
{
    [JsonPropertyName("value")]
    public required T? Value { get; init; }
}
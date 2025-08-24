using System.Text.Json.Serialization;

namespace Core.Session.Elements.Http;

public record FindElementResponse
{
    [JsonPropertyName("element-6066-11e4-a52e-4f735466cecf")]
    public required string ElementIdentifier { get; set; }
}
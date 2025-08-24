using System.Text.Json.Serialization;

namespace Core.Http.Dto.Session;

public class CreateSessionResponse
{
    [JsonPropertyName("sessionId")]
    public string SessionId { get; init; }
}
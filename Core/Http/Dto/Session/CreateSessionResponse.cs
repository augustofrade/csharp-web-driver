using System.Text.Json.Serialization;

namespace Core.Http.Dto.Session;

public class CreateSessionResponse
{
    [JsonPropertyName("value")]
    public SessionData Value { get; init; }
}

public class SessionData
{
    [JsonPropertyName("sessionId")]
    public string SessionId { get; init; }
}
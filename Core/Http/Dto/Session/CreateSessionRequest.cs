using System.Text.Json.Serialization;

namespace Core.Http.Dto.Session;

public class ChromeOptions
{
    [JsonPropertyName("binary")]
    public string Binary { get; init; }
}

public class AlwaysMatch
{
    [JsonPropertyName("browserName")]
    public string BrowserName { get; init; }

    [JsonPropertyName("goog:chromeOptions")]
    public ChromeOptions GoogChromeOptions { get; init; }
}

public class BrowserCapabilities
{
    [JsonPropertyName("alwaysMatch")]
    public AlwaysMatch AlwaysMatch { get; init; }
}

public class CreateSessionRequest
{
    [JsonPropertyName("capabilities")]
    public BrowserCapabilities Capabilities { get; init; }
}
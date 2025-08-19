using System.Text.Json.Serialization;
using Core.Http;
using Core.Http.Dto.Session.Elements;

namespace Core.Session.Elements;

public class SessionElement(string sessionId, string identifier)
{
    private readonly string _sessionId = sessionId;
    private readonly string _identifier = identifier;

    public async Task<SessionElement?> GetElementById(string elementId)
    {
        var body = new Dictionary<string, string>
        {
            { "using", "css selector" },
            { "value", $"#{elementId}" }
        };
        
        var response = await DriverClient.PostAsync<FindElementResponse?>($"/session/{_sessionId}/element/{_identifier}/element", body);
        return new SessionElement(_sessionId, response.ElementIdentifier);
    }
}
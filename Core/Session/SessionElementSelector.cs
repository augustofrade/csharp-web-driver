using Core.Http;
using Core.Http.Dto.Session.Elements;
using Core.Session.Elements;

namespace Core.Session;

public class SessionElementSelector(string sessionId)
{
    private readonly string _sessionId = sessionId;

    public async Task<SessionElement?> ElementById(string elementId)
    {
        var body = new Dictionary<string, string>
        {
            { "using", "css selector" },
            { "value", $"#{elementId}" }
        };
            
        var response = await DriverClient.PostAsync<FindElementResponse?>($"/session/{_sessionId}/element", body);
        return new SessionElement(_sessionId, response.ElementIdentifier);
    }
}
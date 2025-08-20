using System.Text.Json.Serialization;
using Core.Http;
using Core.Http.Dto.Session.Elements;

namespace Core.Session.Elements;

public class SessionElement(string sessionId, string identifier) : ElementSelector(sessionId, $"/session/{sessionId}/element/{identifier}/element")
{
    private readonly string _sessionId = sessionId;
    private readonly string _identifier = identifier;
}
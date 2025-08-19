using Core.Http;

namespace Core;

public class WebDriverSession(string id)
{
    private string Id { get; init; } = id;

    public async Task NavigateTo(string url)
    {
        var response = await DriverClient.PostAsync<dynamic>($"/session/{Id}/url", new
        {
            url = url,
        });
    }
}
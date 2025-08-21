using Core.Http;
using Core.Http.Dto.Session.Windows;

namespace Core.Session.Windows;

public class SessionWindowManager(WebDriverSession session)
{
    private readonly string _baseEndPoint = $"/session/{session.ToString()}/window";
    
    public async Task<SessionWindow> Open(WindowTypes type = WindowTypes.Tab)
    {
        var url = $"{_baseEndPoint}/new";
        var window = (await DriverClient.PostAsync<CreateWindowResponse>(url, new
        {
            type = type == WindowTypes.Tab ? "tab" : "window"
        }))!;
        return new SessionWindow(window.Handle);
    }
 
    public async Task<SessionWindow> Open(string url, WindowTypes type = WindowTypes.Tab)
    {
        var window = await Open(type);
        await SwitchTo(window);
        await session.Location.NavigateTo(url);
        return window;
    }

    public Task Close()
    {
        return DriverClient.DeleteAsync<object>(_baseEndPoint);
    }

    public async Task Close(SessionWindow window)
    {
        await SwitchTo(window);
        await Close();
    }

    public async Task<SessionWindow> SwitchToByIndex(int index)
    {
        var handles = (await GetAllOpen()).ToList();
        if (index < 0 || index >= handles.Count)
            throw new  ArgumentOutOfRangeException(nameof(index));

        var window = handles.ElementAt(index);
        await SwitchTo(window);
        return window;
    }

    public Task SwitchTo(SessionWindow window)
    {
        return DriverClient.PostAsync<object>(_baseEndPoint, new
        {
            handle = window.Handle,
        });
    }

    public async Task<IEnumerable<string>> GetHandles()
    {
        var url = $"{_baseEndPoint}/handles";
        var result = await DriverClient.GetAsync<IEnumerable<string>>(url);
        return result ?? [];
    }

    public async Task<IEnumerable<SessionWindow>> GetAllOpen()
    {
        var result = await GetHandles();
        return result.Select(h => new SessionWindow(h));
    }
}
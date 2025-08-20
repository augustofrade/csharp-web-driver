using Core;
using Core.Drivers;
using DotNetEnv;

namespace Tests;

public class NavigationTests: Base
{
    [Fact]
    public async Task Session_ShouldNavigateTo_DummyPage()
    {
        var session = await InitSession();
        
        await session.Location.NavigateTo("https://www.duckduckgo.com/");
    }


    [Fact]
    public async Task Session_ShouldGet_CurrentURL()
    {
        var session = await InitSession();
        
        await session.Location.NavigateTo("https://duckduckgo.com/");
        var currentUrl = await session.Location.CurrentUrl();
        Assert.Equal("https://duckduckgo.com/", currentUrl);
    }
    
    [Fact]
    public async Task Session_Should_RefreshPage()
    {
        var session = await InitSession();
        
        await session.Location.NavigateTo("https://duckduckgo.com/");
        await Task.Delay(2000);
        await session.Location.Refresh();
        await Task.Delay(2000);
    }
    
    [Fact]
    public async Task Session_ShouldChangeLocation_BackAndForward()
    {
        var session = await InitSession();
        
        await session.Location.NavigateTo("https://duckduckgo.com/");
        await Task.Delay(500);
        await session.Location.NavigateTo("https://github.com/");
        await Task.Delay(1000);
        await session.Location.Back();
        await Task.Delay(2000);
        await session.Location.Forward();
    }
    
    [Fact]
    public async Task Session_ShouldGet_PageTitle()
    {
        var session = await InitSession();
        
        await session.Location.NavigateTo("https://duckduckgo.com/");
        var title = await session.Location.Title();
        Assert.Equal("DuckDuckGo - Protection. Privacy. Peace of mind." , title);
    }
}

using Core.Session.Windows;

namespace Tests;

public class WindowTests : Base
{
    [Fact]
    public async Task Window_ShouldOpen_NewTab()
    {
        var session = await InitSession();
        await session.Windows.Open();
        await Task.Delay(2000);
    }
    
    [Fact]
    public async Task Window_ShouldOpen_NewWindow()
    {
        var session = await InitSession();
        await session.Windows.Open(WindowTypes.Window);
    }
    
    [Fact]
    public async Task Window_ShouldOpenAndSwitchTo_NewTab()
    {
        var session = await InitSession();
        var window = await session.Windows.Open();
        await Task.Delay(500);

        await session.Windows.SwitchTo(window);
    }
    
    [Fact]
    public async Task Window_ShouldNavigateTo_UrlInNewTab()
    {
        var session = await InitSession();
        var window = await session.Windows.Open("https://duckduckgo.com");
        await Task.Delay(2000);
    }
    
    [Fact]
    public async Task Window_ShouldNavigateTo_SecondTab()
    {
        var session = await InitSession();
        
        var secondWindow = await session.Windows.Open();
        await session.Windows.Open();
        await session.Windows.Open();
        await session.Windows.Open();
        
        await Task.Delay(1000);

        var window = await session.Windows.SwitchToByIndex(1);
        Assert.Equal(secondWindow.Handle, window.Handle);
    }
}
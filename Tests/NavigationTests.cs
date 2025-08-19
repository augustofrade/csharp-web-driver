using Core;
using Core.Drivers;
using DotNetEnv;

namespace Tests;

public class NavigationTests
{
    private string DriverPath { get; init; }
    private string BinaryPath { get; init; }
    
    public NavigationTests()
    {
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        var envPath = Path.GetFullPath(Path.Combine(baseDir, "../../../.env"));
        Env.Load(envPath);
        
        DriverPath = Environment.GetEnvironmentVariable("DRIVER_PATH")!;
        BinaryPath = Environment.GetEnvironmentVariable("BINARY_PATH")!;
    }
    
    [Fact]
    public async Task Session_ShouldNavigateTo_DummyPage()
    {
        var driver = new ChromeDriver(new WebDriverOptions(DriverPath, BinaryPath));
        var session = await driver.Start();
        await session.Location.NavigateTo("https://www.duckduckgo.com/");
    }


    [Fact]
    public async Task Session_ShouldGet_CurrentURL()
    {
        var driver = new ChromeDriver(new WebDriverOptions(DriverPath, BinaryPath));
        var session = await driver.Start();
        await session.Location.NavigateTo("https://duckduckgo.com/");
        var currentUrl = await session.Location.CurrentUrl();
        Assert.Equal("https://duckduckgo.com/", currentUrl);
    }
    
    [Fact]
    public async Task Session_Should_RefreshPage()
    {
        var driver = new ChromeDriver(new WebDriverOptions(DriverPath, BinaryPath));
        var session = await driver.Start();
        await session.Location.NavigateTo("https://duckduckgo.com/");
        await Task.Delay(2000);
        await session.Location.Refresh();
        await Task.Delay(2000);
    }
    
    [Fact]
    public async Task Session_ShouldChangeLocation_BackAndForward()
    {
        var driver = new ChromeDriver(new WebDriverOptions(DriverPath, BinaryPath));
        var session = await driver.Start();
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
        var driver = new ChromeDriver(new WebDriverOptions(DriverPath, BinaryPath));
        var session = await driver.Start();
        await session.Location.NavigateTo("https://duckduckgo.com/");
        var title = await session.Location.Title();
        Assert.Equal("DuckDuckGo - Protection. Privacy. Peace of mind." , title);
    }
}

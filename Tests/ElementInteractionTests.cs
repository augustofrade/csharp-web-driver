using Core;
using Core.Drivers;
using DotNetEnv;

namespace Tests;

public class ElementInteractionTests
{
    private string DriverPath { get; init; }
    private string BinaryPath { get; init; }
    
    public ElementInteractionTests()
    {
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        var envPath = Path.GetFullPath(Path.Combine(baseDir, "../../../.env"));
        Env.Load(envPath);
        
        DriverPath = Environment.GetEnvironmentVariable("DRIVER_PATH")!;
        BinaryPath = Environment.GetEnvironmentVariable("BINARY_PATH")!;
    }
    
    [Fact]
    public async Task Session_ShouldInteractBy_TypingAndClicking()
    {
        var driver = new ChromeDriver(new WebDriverOptions(DriverPath, BinaryPath));
        var session = await driver.Start();
        await session.Location.NavigateTo("https://duckduckgo.com/");
        await Task.Delay(1000);
        
        var searchBox = await session.Dom.GetElementById("searchbox_input");
        await searchBox.SendKeys("github");
        
        var searchButton = await session.Dom.QuerySelector("#searchbox_homepage button:last-child");
        await searchButton.Click();
        
        await Task.Delay(2000);
    }
}
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
    
    [Fact]
    public async Task Session_ShouldGetElement_Text()
    {
        var driver = new ChromeDriver(new WebDriverOptions(DriverPath, BinaryPath));
        var session = await driver.Start();
        await session.Location.NavigateTo("https://duckduckgo.com/");
        await Task.Delay(1000);

        var titleEl = await session.Dom.QuerySelector("section h2");
        var title = await  titleEl.GetText();
        Assert.Equal("Switch to DuckDuckGo. It’s private and free!", title);
    }
    
    [Fact]
    public async Task Session_ShouldGetHackerNews_SubmissionTitles()
    {
        var driver = new ChromeDriver(new WebDriverOptions(DriverPath, BinaryPath));
        var session = await driver.Start();
        await session.Location.NavigateTo("https://news.ycombinator.com/");
        await Task.Delay(1000);

        List<string> titleList = [];
        
        foreach (var submission in await session.Dom.QuerySelectorAll(".submission"))
        {
            var titleEl = await submission.QuerySelector(".titleline");
            var title = await  titleEl!.GetText();
            titleList.Add(title);
        }
        
        Assert.NotEmpty(titleList);
    }
}
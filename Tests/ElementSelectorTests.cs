using Core;
using Core.Drivers;
using DotNetEnv;

namespace Tests;

public class ElementSelectorTests
{
    private string DriverPath { get; init; }
    private string BinaryPath { get; init; }
    
    public ElementSelectorTests()
    {
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        var envPath = Path.GetFullPath(Path.Combine(baseDir, "../../../.env"));
        Env.Load(envPath);
        
        DriverPath = Environment.GetEnvironmentVariable("DRIVER_PATH")!;
        BinaryPath = Environment.GetEnvironmentVariable("BINARY_PATH")!;
    }
    
    [Fact]
    public async Task Session_ShouldFindElementBy_Id()
    {
        var driver = new ChromeDriver(new WebDriverOptions(DriverPath, BinaryPath));
        var session = await driver.Start();
        await session.Location.NavigateTo("https://duckduckgo.com/");
        await Task.Delay(1000);
        
        var form = await session.Dom.GetElementById("searchbox_homepage");
        var searchBox = form != null ? await form.GetElementById("searchbox_input") : null;
        Assert.NotNull(searchBox);
    }
    
    [Fact]
    public async Task Session_ShouldFindElementBy_TagName()
    {
        var driver = new ChromeDriver(new WebDriverOptions(DriverPath, BinaryPath));
        var session = await driver.Start();
        await session.Location.NavigateTo("https://duckduckgo.com/");
        await Task.Delay(1000);
        
        var element = await session.Dom.GetElementByTagName("h2");
        Assert.NotNull(element);
    }
    
    [Fact]
    public async Task Session_ShouldFindElementBy_ClassName()
    {
        var driver = new ChromeDriver(new WebDriverOptions(DriverPath, BinaryPath));
        var session = await driver.Start();
        await session.Location.NavigateTo("https://duckduckgo.com/");
        await Task.Delay(1000);
        
        var element = await session.Dom.GetElementByClassName("text-center");
        Assert.NotNull(element);
    }
    
    [Fact]
    public async Task Session_ShouldFindElementBy_QuerySelector()
    {
        var driver = new ChromeDriver(new WebDriverOptions(DriverPath, BinaryPath));
        var session = await driver.Start();
        await session.Location.NavigateTo("https://duckduckgo.com/");
        await Task.Delay(1000);
        
        var element = await session.Dom.QuerySelector("section h2");
        Assert.NotNull(element);
    }
    
    [Fact]
    public async Task Session_ShouldFindAllElementsBy_QuerySelector()
    {
        var driver = new ChromeDriver(new WebDriverOptions(DriverPath, BinaryPath));
        var session = await driver.Start();
        await session.Location.NavigateTo("https://duckduckgo.com/");
        await Task.Delay(1000);
        
        var elements = await session.Dom.QuerySelectorAll("ul.flex li");
        Assert.NotEmpty(elements);
    }
    
    [Fact]
    public async Task Session_ShouldFindElementBy_XPath()
    {
        var driver = new ChromeDriver(new WebDriverOptions(DriverPath, BinaryPath));
        var session = await driver.Start();
        await session.Location.NavigateTo("https://duckduckgo.com/");
        await Task.Delay(1000);
        
        var element = await session.Dom.GetElementByXPath("//article//h1");
        Assert.NotNull(element);
    }
}
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
    public async Task Session_ShouldFindElement_ById()
    {
        var driver = new ChromeDriver(new WebDriverOptions(DriverPath, BinaryPath));
        var session = await driver.Start();
        await session.Location.NavigateTo("https://duckduckgo.com/");
        await Task.Delay(1000);
        var form = await session.Get.ElementById("searchbox_homepage");
        var searchBox = form != null ? await form.GetElementById("searchbox_input") : null;
        Assert.NotNull(searchBox);
    }
}
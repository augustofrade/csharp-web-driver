using Core;
using Core.Drivers;
using DotNetEnv;

namespace Tests;

public class WebDriverTests
{
    private string DriverPath { get; init; }
    private string BinaryPath { get; init; }
    
    public WebDriverTests()
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
        await session.NavigateTo("https://www.duckduckgo.com/");
    }
}

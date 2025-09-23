using Core;
using Core.Drivers;

namespace Tests;

public class ExtensionTests : Base
{
    [Fact]
    public async Task Session_ShouldLoad_Extension()
    {
        var homePath = Environment.GetEnvironmentVariable("HOME_PATH")!;
        var extensionPath = Path.GetFullPath(Path.Combine(homePath, "extension.crx"));
        
        var driver = new ChromeDriver(new WebDriverOptions(DriverPath, BinaryPath))
            .WithExtension(extensionPath);
        
        var session = await driver.Start();
        session.Location.NavigateTo("https://duckduckgo.com/");
        await Task.Delay(1000);

        // The session has been created successfully
        var pageUrl = await session.Location.CurrentUrl(); 
        Assert.Equal("https://duckduckgo.com/", pageUrl);

    }
}
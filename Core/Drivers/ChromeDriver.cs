namespace Core.Drivers;

/// <summary>
/// Default ChromeDriver initialization class
/// </summary>
/// <param name="options"></param>
public class ChromeDriver(WebDriverOptions options) : WebDriver(options)
{
    protected override string BrowserName => "chrome";
}
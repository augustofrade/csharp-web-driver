namespace Core.Drivers;

public class ChromeDriver(WebDriverOptions options) : WebDriver(options)
{
    protected override string BrowserName => "chrome";
}
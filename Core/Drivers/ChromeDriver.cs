namespace Core.Drivers;

public class ChromeDriver(WebDriverOptions options) : WebDriver(options)
{
    public override string BrowserName => "chrome";
}
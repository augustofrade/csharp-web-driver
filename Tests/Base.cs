using Core;
using Core.Drivers;
using DotNetEnv;

namespace Tests;

public class Base
{
    private string DriverPath { get; init; }
    private string BinaryPath { get; init; }
    
    public Base()
    {
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        var envPath = Path.GetFullPath(Path.Combine(baseDir, "../../../.env"));
        Env.Load(envPath);
        
        DriverPath = Environment.GetEnvironmentVariable("DRIVER_PATH")!;
        BinaryPath = Environment.GetEnvironmentVariable("BINARY_PATH")!;
    }

    public Task<WebDriverSession> InitSession()
    {
        var driver = new ChromeDriver(new WebDriverOptions(DriverPath, BinaryPath));
        return driver.Start();
    }
}
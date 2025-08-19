using System.Diagnostics;
using Core.Http;
using Core.Http.Dto.Session;

namespace Core;

public abstract class WebDriver
{
    private string WebDriverPath { get; init; }
    private string BrowserBinaryPath { get; init; }

    private int Port { get; set; }
    
    public abstract string BrowserName { get; }

    public WebDriver(WebDriverOptions options)
    {
        WebDriverPath = options.WebDriverPath;
        BrowserBinaryPath = options.BrowserBinaryPath;
    }

    public async Task<WebDriverSession> Start(int port = 4444)
    {
        Console.WriteLine($"Starting web driver at {WebDriverPath}");
        Port = port;
        try
        {
            Process.Start(WebDriverPath, $"--port={port}");
            return await CreateSession();
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to start web driver at {WebDriverPath}.\n{ex.Message}");
        }
    }

    private async Task<WebDriverSession> CreateSession()
    {
        var body = new
        {
            capabilities = new
            {
                alwaysMatch = new Dictionary<string, object>
                {
                    { "browserName", BrowserName },
                    { "goog:chromeOptions", new
                        {
                            binary = BrowserBinaryPath,
                        }
                    }
                }
            }
        };
        
        var data = await DriverClient.PostAsync<CreateSessionResponse>("/session", body);
        return new WebDriverSession(data!.SessionId);
    }

    
}
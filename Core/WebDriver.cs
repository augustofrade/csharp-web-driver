using System.Diagnostics;
using Core.Http;
using Core.Http.Dto.Session;

namespace Core;

public abstract class WebDriver(WebDriverOptions options)
{
    private string WebDriverPath { get; init; } = options.WebDriverPath;
    private string BrowserBinaryPath { get; init; } = options.BrowserBinaryPath;

    private int Port { get; set; }

    protected abstract string BrowserName { get; }

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
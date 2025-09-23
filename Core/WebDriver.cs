using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Core.Http;
using Core.Http.Dto.Session;

namespace Core;

/// <summary>
/// WebDriver initialization.
/// <br/>
/// Contains generic web driver initialization and session creation logic. 
/// </summary>
/// <param name="options"></param>
public abstract class WebDriver(WebDriverOptions options)
{
    /// <summary>
    /// Path to the Web Driver binary in the file system.
    /// </summary>
    private string WebDriverPath { get; init; } = options.WebDriverPath;
    
    /// <summary>
    /// Path to the Browser binary in the file system.
    /// </summary>
    private string BrowserBinaryPath { get; init; } = options.BrowserBinaryPath;

    /// <summary>
    /// What port should the Web Driver use.
    /// </summary>
    private int Port { get; set; }

    /// <summary>
    /// Extension list provided by the user
    /// </summary>
    protected List<string> Extensions { get; init; } = [];

    /// <summary>
    /// Name of the browser to be used during the session initialization.
    /// <br/>
    /// A value is declared by WebDriver child classes.
    /// </summary>
    protected abstract string BrowserName { get; }

    /// <summary>
    /// Initializes the Web Driver.
    /// </summary>
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

    /// <summary>
    /// Adds an extension to the session.
    /// <br/>
    /// <paramref name="path"/> must be the path to a packaged browser extension.
    /// </summary>
    public WebDriver WithExtension(string path)
    {
        var fullPath = Path.GetFullPath(path);
        if(!Path.Exists(fullPath))
            throw new FileNotFoundException($"Packaged extension not found: \"{fullPath}\"");
        
        var pathAttributes = File.GetAttributes(fullPath);
        if(pathAttributes.HasFlag(FileAttributes.Directory))
            throw new Exception($"Expected file, found directory: \"{fullPath}\"");
        
        Extensions.Add(fullPath);

        return this;
    }

    
    /// <summary>
    /// Creates a session with the running Web Driver.
    /// </summary>
    private async Task<WebDriverSession> CreateSession()
    {
        var base64Extensions = new List<string>();
        foreach (var extension in Extensions)
        {
            var fileContent = await File.ReadAllBytesAsync(extension);
            var base64 = Convert.ToBase64String(fileContent);
            base64Extensions.Add(base64);
        }
        
        // TODO: handle session creation errors
        
        var body = new
        {
            capabilities = new
            {
                alwaysMatch = new Dictionary<string, object>
                {
                    { "browserName", BrowserName },
                    { "goog:chromeOptions", new
                        {
                            extensions = base64Extensions,
                            binary = BrowserBinaryPath,
                        }
                    }
                }
            }
        };

        var x = JsonSerializer.Serialize(body);
        
        var data = await DriverClient.PostAsync<CreateSessionResponse>("/session", body);
        return new WebDriverSession(data!.SessionId);
    }

    
}
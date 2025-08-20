using Core;
using Core.Drivers;
using DotNetEnv;

namespace Tests;

public class ElementInteractionTests : Base
{
    [Fact]
    public async Task Session_ShouldInteractBy_TypingAndClicking()
    {
        var session = await InitSession();
        
        await session.Location.NavigateTo("https://duckduckgo.com/");
        await Task.Delay(1000);
        
        var searchBox = await session.Dom.GetElementById("searchbox_input");
        await searchBox.SendKeys("github");
        
        var searchButton = await session.Dom.QuerySelector("#searchbox_homepage button:last-child");
        await searchButton.Click();
        
        await Task.Delay(2000);
    }
    
    [Fact]
    public async Task Session_ShouldGetElement_Text()
    {
        var session = await InitSession();
        
        await session.Location.NavigateTo("https://duckduckgo.com/");
        await Task.Delay(1000);

        var titleEl = await session.Dom.QuerySelector("section h2");
        var title = titleEl.TextContent;
        Assert.Equal("Switch to DuckDuckGo. It’s private and free!", title);
    }
    
    [Fact]
    public async Task Session_ShouldGetElement_TextAsync()
    {
        var session = await InitSession();
        
        await session.Location.NavigateTo("https://duckduckgo.com/");
        await Task.Delay(1000);

        var titleEl = await session.Dom.QuerySelector("section h2");
        var title = await titleEl.GetTextAsync();
        Assert.Equal("Switch to DuckDuckGo. It’s private and free!", title);
    }
    
    [Fact]
    public async Task Session_ShouldGetElement_AttributeAsync()
    {
        var session = await InitSession();
        
        await session.Location.NavigateTo("https://duckduckgo.com/");
        await Task.Delay(1000);

        var html = await session.Dom.QuerySelector("html");
        var pageLanguage = await html.GetAttributeAsync("lang");
        Assert.Equal("en-US", pageLanguage);
    }
    
    [Fact]
    public async Task Session_ShouldGetHackerNews_SubmissionTitles()
    {
        var session = await InitSession();
        
        await session.Location.NavigateTo("https://news.ycombinator.com/");
        await Task.Delay(1000);

        List<string> titleList = [];
        
        foreach (var submission in await session.Dom.QuerySelectorAll(".submission"))
        {
            var titleEl = await submission.QuerySelector(".titleline");
            var title = await  titleEl!.GetTextAsync();
            titleList.Add(title);
        }
        
        Assert.NotEmpty(titleList);
    }
}
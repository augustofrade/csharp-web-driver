using Core;
using Core.Drivers;
using Core.Session.Elements.Options;
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
        
        session.Dom.GetElementById("searchbox_input")!.SendKeys("github");
        
        session.Dom.QuerySelector("#searchbox_homepage button:last-child")!.Click();
        
        await Task.Delay(2000);
    }
    
    [Fact]
    public async Task Session_ShouldGetElement_Text()
    {
        var session = await InitSession();
        
        await session.Location.NavigateTo("https://duckduckgo.com/");
        await Task.Delay(1000);

        var titleEl = session.Dom.QuerySelector("section h2")!;
        var title = titleEl.TextContent;
        Assert.Equal("Switch to DuckDuckGo. It’s private and free!", title);
    }
    
    [Fact]
    public async Task Session_ShouldGetElement_TextAsync()
    {
        var session = await InitSession();
        
        await session.Location.NavigateTo("https://duckduckgo.com/");
        await Task.Delay(1000);

        var titleEl = session.Dom.QuerySelector("section h2")!;
        var title = await titleEl.GetTextAsync();
        Assert.Equal("Switch to DuckDuckGo. It’s private and free!", title);
    }
    
    [Fact]
    public async Task Session_ShouldGetElement_AttributeAsync()
    {
        var session = await InitSession();
        
        await session.Location.NavigateTo("https://duckduckgo.com/");
        await Task.Delay(1000);

        var html = session.Dom.QuerySelector("html")!;
        var pageLanguage = await html.GetAttributeAsync("lang");
        Assert.Equal("en-US", pageLanguage);
    }
    
    [Fact]
    public async Task Session_ShouldGetElement_PropertyAsync()
    {
        var session = await InitSession();
        
        await session.Location.NavigateTo("https://old.reddit.com/r/ProgrammerHumor/");
        await Task.Delay(1000);

        session.Dom.QuerySelector("[name='q']")!.Click();
        await Task.Delay(1000);
        
        var checkbox = session.Dom.QuerySelector("[name='restrict_sr']")!;
        checkbox.Click();
        var isChecked = await checkbox.GetPropertyAsync<bool>("checked");
        Assert.True(isChecked);
    }
    
    [Fact]
    public async Task Session_ShouldGetElement_ClassList()
    {
        var session = await InitSession();

        await session.Location.NavigateTo("https://news.ycombinator.com/");
        
        var classList = session.Dom.GetElementByClassName("submission")!.ClassList;
        Assert.Contains("athing", classList);
    }
    
    [Fact]
    public async Task Session_ShouldGetElement_TagName()
    {
        var session = await InitSession();

        await session.Location.NavigateTo("https://news.ycombinator.com/");
        var tagName = session.Dom.GetElementById("hnmain")!.TagName;
        Assert.Contains("table", tagName);
    }

    [Fact]
    public async Task Session_ElementShould_ExecuteScript()
    {
        var session = await InitSession();
        
        await session.Location.NavigateTo("https://news.ycombinator.com/");

        var body = session.Dom.QuerySelector("body")!;
        var concatenation = await body.ExecuteJs<string>("return arguments[1] + arguments[2];", 1, "abc");
        Assert.Equal("1abc", concatenation);
    }
    
    [Fact]
    public async Task Session_ShouldGetHackerNews_SubmissionTitles()
    {
        var session = await InitSession();
        
        await session.Location.NavigateTo("https://news.ycombinator.com/");
        await Task.Delay(1000);

        List<string> titleList = [];
        
        foreach (var submission in session.Dom.QuerySelectorAll(".submission"))
        {
            var title = await submission.QuerySelector(".titleline")!.GetTextAsync();
            titleList.Add(title);
        }
        
        Assert.NotEmpty(titleList);
    }

    [Fact]
    public async Task Session_ShouldScrollIntoView_Element()
    {
        var session = await InitSession();
        await session.Location.NavigateTo("https://news.ycombinator.com/newcomments");
        await Task.Delay(1000);
        
        var row = session.Dom.QuerySelectorAll("tr.athing").ElementAt(10);
        row.ScrollIntoView(new ElementAlignToTopOptions
        {
            Behavior = ElementAlignToTopBehavior.Smooth
        });

        await Task.Delay(1000);
        
        var scrollPos = session.Context.Rect.Get().y;

        Assert.True(scrollPos > 0);
    }
}
using Core;
using Core.Drivers;
using DotNetEnv;

namespace Tests;

public class ElementSelectorTests : Base
{
    
    [Fact]
    public async Task Session_ShouldFindElementBy_Id()
    {
        var session = await InitSession();
        
        await session.Location.NavigateTo("https://duckduckgo.com/");
        await Task.Delay(1000);
        
        var form = session.Dom.GetElementById("searchbox_homepage");
        var searchBox = form != null ? form.GetElementById("searchbox_input") : null;
        Assert.NotNull(searchBox);
    }
    
    [Fact]
    public async Task Session_ShouldFindElementBy_TagName()
    {
        var session = await InitSession();
        
        await session.Location.NavigateTo("https://duckduckgo.com/");
        await Task.Delay(1000);
        
        var element = session.Dom.GetElementByTagName("h2");
        Assert.NotNull(element);
    }
    
    [Fact]
    public async Task Session_ShouldFindElementBy_ClassName()
    {
        var session = await InitSession();
        
        await session.Location.NavigateTo("https://duckduckgo.com/");
        await Task.Delay(1000);
        
        var element = session.Dom.GetElementByClassName("text-center");
        Assert.NotNull(element);
    }
    
    [Fact]
    public async Task Session_ShouldFindElementBy_QuerySelector()
    {
        var session = await InitSession();
        
        await session.Location.NavigateTo("https://duckduckgo.com/");
        await Task.Delay(1000);
        
        var element = session.Dom.QuerySelector("section h2");
        Assert.NotNull(element);
    }
    
    [Fact]
    public async Task Session_ShouldFindAllElementsBy_QuerySelector()
    {
        var session = await InitSession();
        
        await session.Location.NavigateTo("https://duckduckgo.com/");
        await Task.Delay(1000);
        
        var elements = session.Dom.QuerySelectorAll("ul.flex li");
        Assert.NotEmpty(elements);
    }
    
    [Fact]
    public async Task Session_ShouldFindElementBy_XPath()
    {
        var session = await InitSession();
        
        await session.Location.NavigateTo("https://duckduckgo.com/");
        await Task.Delay(1000);
        
        var element = session.Dom.GetElementByXPath("//article//h1");
        Assert.NotNull(element);
    }
    
    [Fact]
    public async Task Session_ShouldFindElement_ChildrenAsync()
    {
        var session = await InitSession();
        
        await session.Location.NavigateTo("https://news.ycombinator.com/");
        await Task.Delay(1000);
        
        var element = session.Dom.GetElementByClassName("yclinks")!;
        var children = await element.GetChildrenAsync();
        Assert.NotEmpty(children);
    }
    
    [Fact]
    public async Task Session_ShouldFindElement_Children()
    {
        var session = await InitSession();
        
        await session.Location.NavigateTo("https://news.ycombinator.com/");
        await Task.Delay(1000);
        
        var element = session.Dom.GetElementByClassName("yclinks")!;
        var children = element.Children;
        Assert.NotEmpty(children);
    }
}
namespace Tests;

public class WindowContextTests : Base
{
    [Fact]
    public async Task WindowContext_ShouldSwitchTo_Frame()
    {
        var session = await InitSession();
        await session.Location.NavigateTo("https://www.w3schools.com/jsref/tryit.asp?filename=tryjsref_alert");
        await Task.Delay(1000);

        var iframe = session.Dom.GetElementById("iframeResult")!;
        await session.Context.SwitchToFrame(iframe);

        var innerTitle = session.Dom.QuerySelector("h1")?.TextContent;
        
        Assert.Equal("The Window Object", innerTitle);
    }
    
    [Fact]
    public async Task Window_ShouldSwitchTo_FrameAndReset()
    {
        var session = await InitSession();
        await session.Location.NavigateTo("https://www.w3schools.com/jsref/tryit.asp?filename=tryjsref_alert");
        await Task.Delay(1000);

        var iframe = session.Dom.GetElementById("iframeResult")!;
        await session.Context.SwitchToFrame(iframe);
        await session.Context.Reset();

        var innerTitle = session.Dom.QuerySelector("h1")?.TextContent;
        
        Assert.Null(innerTitle);
    }
    
    [Fact]
    public async Task Window_ShouldSwitchTo_FrameAndBack()
    {
        var session = await InitSession();
        await session.Location.NavigateTo("https://www.w3schools.com/jsref/tryit.asp?filename=tryjsref_alert");
        await Task.Delay(1000);

        var iframe = session.Dom.GetElementById("iframeResult")!;
        await session.Context.SwitchToFrame(iframe);
        await session.Context.SwitchToParentFrame();

        var innerTitle = session.Dom.QuerySelector("h1")?.TextContent;
        
        Assert.Null(innerTitle);
    }
    
    [Fact]
    public async Task TopLevelContext_ShouldGet_WindowRect()
    {
        var session = await InitSession();

        var rect = session.Context.Rect.Get();
    }
    
    [Fact]
    public async Task TopLevelContext_ShouldSet_WindowSize()
    {
        var session = await InitSession();

        var rect = session.Context.Rect.SetSize(1000, 800);
        Assert.Equal(1000, rect.width);
        Assert.Equal(800, rect.height);
    }
    
    [Fact]
    public async Task TopLevelContext_ShouldSet_WindowPosition()
    {
        var session = await InitSession();
        
        session.Context.Rect.SetSize(1000, 800);
        var rect = session.Context.Rect.SetPosition(500, 200);
        Assert.Equal(500, rect.x);
        Assert.Equal(200, rect.y);
    }
    
    [Fact]
    public async Task TopLevelContext_ShouldSet_WindowRect()
    {
        var session = await InitSession();

        var rect = session.Context.Rect.Set(500, 200, 1000, 800);
        Assert.Equal(500, rect.x);
        Assert.Equal(200, rect.y);
        Assert.Equal(1000, rect.width);
        Assert.Equal(800, rect.height);
    }
}
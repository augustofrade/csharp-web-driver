namespace Tests;

public class WindowContextTests : Base
{
    [Fact]
    public async Task WindowContext_ShouldSwitchTo_Frame()
    {
        var session = await InitSession();
        await session.Location.NavigateTo("https://www.w3schools.com/jsref/tryit.asp?filename=tryjsref_alert");
        await Task.Delay(1000);

        var iframe = await session.Dom.GetElementById("iframeResult");
        await session.Context.SwitchToFrame(iframe!);

        var innerTitleEl = await session.Dom.QuerySelector("h1");
        var title = innerTitleEl?.TextContent;
        
        Assert.Equal("The Window Object", title);
    }
    
    [Fact]
    public async Task Window_ShouldSwitchTo_FrameAndReset()
    {
        var session = await InitSession();
        await session.Location.NavigateTo("https://www.w3schools.com/jsref/tryit.asp?filename=tryjsref_alert");
        await Task.Delay(1000);

        var iframe = await session.Dom.GetElementById("iframeResult");
        await session.Context.SwitchToFrame(iframe!);
        await session.Context.Reset();

        var innerTitleEl = await session.Dom.QuerySelector("h1");
        var title = innerTitleEl?.TextContent;
        
        Assert.Null(title);
    }
    
    [Fact]
    public async Task Window_ShouldSwitchTo_FrameAndBack()
    {
        var session = await InitSession();
        await session.Location.NavigateTo("https://www.w3schools.com/jsref/tryit.asp?filename=tryjsref_alert");
        await Task.Delay(1000);

        var iframe = await session.Dom.GetElementById("iframeResult");
        await session.Context.SwitchToFrame(iframe!);
        await session.Context.SwitchToParentFrame();

        var innerTitleEl = await session.Dom.QuerySelector("h1");
        var title = innerTitleEl?.TextContent;
        
        Assert.Null(title);
    }
}
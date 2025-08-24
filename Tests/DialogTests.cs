namespace Tests;

public class DialogTests : Base
{
    [Fact]
    public async Task Alert_ShouldBe_Accepted()
    {
        var session = await InitSession();
        await session.Location.NavigateTo("https://www.w3schools.com/jsref/tryit.asp?filename=tryjsref_alert");
        await Task.Delay(1000);

        var iframe = session.Dom.GetElementById("iframeResult");
        await session.Context.SwitchToFrame(iframe!);

        session.Dom.QuerySelector("button")!.Click();
        await Task.Delay(500);

        await session.Alert.Accept();
    }
    
    [Fact]
    public async Task Prompt_Should_Be_SentText()
    {
        var session = await InitSession();
        await session.Location.NavigateTo("https://www.w3schools.com/jsref/tryit.asp?filename=tryjsref_prompt");
        await Task.Delay(1000);

        var iframe = session.Dom.GetElementById("iframeResult");
        await session.Context.SwitchToFrame(iframe!);

        session.Dom.QuerySelector("button")!.Click();
        await Task.Delay(500);

        await session.Prompt.SendText("Bob");
        await session.Prompt.Accept();

        var text = session.Dom.GetElementById("demo")?.TextContent;
        
        Assert.Equal("Hello Bob! How are you today?", text);
    }
    
    [Fact]
    public async Task Prompt_Should_Be_Dismissed()
    {
        var session = await InitSession();
        await session.Location.NavigateTo("https://www.w3schools.com/jsref/tryit.asp?filename=tryjsref_prompt");
        await Task.Delay(1000);

        var iframe = session.Dom.GetElementById("iframeResult");
        await session.Context.SwitchToFrame(iframe!);

        session.Dom.QuerySelector("button")!.Click();
        await Task.Delay(500);

        await session.Prompt.Dismiss();

        var title = session.Dom.QuerySelector("h2")?.TextContent;
        
        Assert.Equal("The prompt() Method", title);
    }
}
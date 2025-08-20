namespace Tests;

public class DialogTests : Base
{
    [Fact]
    public async Task Alert_ShouldBe_Accepted()
    {
        var session = await InitSession();
        await session.Location.NavigateTo("https://www.w3schools.com/jsref/tryit.asp?filename=tryjsref_alert");
        await Task.Delay(1000);

        var iframe = await session.Dom.GetElementById("iframeResult");
        await session.Context.SwitchToFrame(iframe!);

        var button = await session.Dom.QuerySelector("button");
        await button!.Click();
        await Task.Delay(500);

        await session.Alert.Accept();
    }
    
    [Fact]
    public async Task Prompt_Should_Be_SentText()
    {
        var session = await InitSession();
        await session.Location.NavigateTo("https://www.w3schools.com/jsref/tryit.asp?filename=tryjsref_prompt");
        await Task.Delay(1000);

        var iframe = await session.Dom.GetElementById("iframeResult");
        await session.Context.SwitchToFrame(iframe!);

        var button = await session.Dom.QuerySelector("button");
        await button!.Click();
        await Task.Delay(500);

        await session.Prompt.SendText("Bob");
        await session.Prompt.Accept();

        var resultEl = await session.Dom.GetElementById("demo");
        var text = resultEl?.TextContent;
        
        Assert.Equal("Hello Bob! How are you today?", text);
    }
    
    [Fact]
    public async Task Prompt_Should_Be_Dismissed()
    {
        var session = await InitSession();
        await session.Location.NavigateTo("https://www.w3schools.com/jsref/tryit.asp?filename=tryjsref_prompt");
        await Task.Delay(1000);

        var iframe = await session.Dom.GetElementById("iframeResult");
        await session.Context.SwitchToFrame(iframe!);

        var button = await session.Dom.QuerySelector("button");
        await button!.Click();
        await Task.Delay(500);

        await session.Prompt.Dismiss();

        var titleEl = await session.Dom.QuerySelector("h2");
        var title = titleEl?.TextContent;
        
        Assert.Equal("The prompt() Method", title);
    }
}
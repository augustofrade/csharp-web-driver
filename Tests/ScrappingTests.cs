using System.Text.RegularExpressions;
using Core.Session.Elements;

namespace Tests;


public class HackerNewsSubmission
{
    public string Title { get; set; } = string.Empty;
    public int Score { get; set; }
    public string Url { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int Comments { get; set; }
}

public class ScrappingTests : Base
{
    [Fact]
    public async void Scrapper_ShouldScrap_HomePage()
    {
        var session = await InitSession();

        await session.Location.NavigateTo("https://news.ycombinator.com/");

        List<HackerNewsSubmission> submissions = [];
        
        const int maxPages = 3;
        
        for (var page = 1; page <= maxPages; page++)
        {
            var submissionElements = (await session.Dom.QuerySelectorAll(".submission")).ToList();
            var submissionInfoElements = (await session.Dom.QuerySelectorAll(".submission + tr")).ToList();
            
            for (var i = 0; i < submissionElements.Count; i++)
            {
                var submission = submissionElements[i];
                var subline = submissionInfoElements[i];

                var sub = new HackerNewsSubmission();

                var titleElement = await submission.QuerySelector(".titleline");
                if (titleElement != null)
                    sub.Title = await titleElement.GetText();

                var scoreElement = await subline.QuerySelector(".score");
                if (scoreElement != null)
                {
                    var scoreRaw = await scoreElement.GetText();
                    var scoreMatch = Regex.Match(scoreRaw, @"(\d+)");
                    sub.Score = scoreMatch.Success ? int.Parse(scoreMatch.Groups[1].Value) : 0;
                }

                var authorElement = await subline.QuerySelector(".hnuser");
                if(authorElement != null)
                    sub.Author = await authorElement.GetText();
                
                var commentsElement = await subline.QuerySelector(".subline > a:last-child");
                if (commentsElement != null)
                {
                    var commentsRaw = await commentsElement.GetText();
                    var commentsMatch = Regex.Match(commentsRaw, @"(\d+)");
                    sub.Comments = commentsMatch.Success ? int.Parse(commentsMatch.Groups[1].Value) : 0;
                }
                
                submissions.Add(sub);
            }

            if (page >= maxPages) continue;
            
            var moreBtn = await session.Dom.QuerySelector(".morelink");
            await moreBtn.Click();
            await Task.Delay(1000);
        }
        
        Assert.Equal(maxPages * 30, submissions.Count);
    }
}
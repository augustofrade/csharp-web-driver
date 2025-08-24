using System.Text.RegularExpressions;
using Core.Session.Elements;

namespace Tests;


public class HackerNewsAuthor
{
    public string Name { get; private init; }
    public string Url { get; private init; }

    public HackerNewsAuthor(string name, string url)
    {
        Name = name;
        Url = $"https://news.ycombinator.com/{url}";
    }
}

public class HackerNewsSubmission
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Score { get; set; }
    public string Url { get; set; } = string.Empty;
    public HackerNewsAuthor Author { get; set; }
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
        const int maxPages = 10;
        
        for (var page = 1; page <= maxPages; page++)
        {
            var submissionElements = session.Dom.QuerySelectorAll(".submission").ToList();
            var submissionInfoElements = session.Dom.QuerySelectorAll(".submission + tr").ToList();
            
            for (var i = 0; i < submissionElements.Count; i++)
            {
                var submission = submissionElements[i];
                var subline = submissionInfoElements[i];

                var sub = new HackerNewsSubmission();

                var id = submission.Id!;
                sub.Id = int.Parse(id);
                var submissionAnchor = submission.QuerySelector(".titleline a");
                sub.Url = (await submissionAnchor!.GetAttributeAsync("href"))!;

                var titleElement = submission.QuerySelector(".titleline");
                if (titleElement != null)
                    sub.Title = titleElement.TextContent;

                var scoreElement = subline.QuerySelector(".score");
                if (scoreElement != null)
                {
                    var scoreRaw = scoreElement.TextContent;
                    var scoreMatch = Regex.Match(scoreRaw, @"(\d+)");
                    sub.Score = scoreMatch.Success ? int.Parse(scoreMatch.Groups[1].Value) : 0;
                }

                var authorElement = subline.QuerySelector(".hnuser");
                if (authorElement != null)
                {
                    var authorName = authorElement.TextContent;
                    var authorPage = await authorElement.GetAttributeAsync("href");
                    sub.Author = new HackerNewsAuthor(authorName, authorPage!);
                }
                
                var commentsElement = subline.QuerySelector(".subline > a:last-child");
                if (commentsElement != null)
                {
                    var commentsRaw = commentsElement.TextContent;
                    var commentsMatch = Regex.Match(commentsRaw, @"(\d+)");
                    sub.Comments = commentsMatch.Success ? int.Parse(commentsMatch.Groups[1].Value) : 0;
                }
                
                submissions.Add(sub);
            }

            if (page >= maxPages) continue;
            
            session.Dom.QuerySelector(".morelink")!.Click();
            await Task.Delay(1000);
        }
        
        Assert.Equal(maxPages * 30, submissions.Count);
    }
}
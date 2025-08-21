using Core.Http;
using Core.Session;
using Core.Session.Dialogs;
using Core.Session.Elements;
using Core.Session.Windows;

namespace Core;

public class WebDriverSession
{
    private readonly string _id;
    
    public SessionLocation Location;
    
    public IElementSelector Dom;
    
    public SessionContext Context;

    public SessionWindowManager Windows;
    
    public ISimpleDialog Alert;
    
    public ISimpleDialog Confirm;
    
    public PromptDialog Prompt;

    public WebDriverSession(string id)
    {
        _id = id;
        Location = new SessionLocation(id);
        Dom = new SessionElementSelector(id);
        Context = new SessionContext(_id);
        Windows = new SessionWindowManager(this);
        Alert = new SimpleDialog(_id);
        Confirm = new SimpleDialog(_id);
        Prompt = new PromptDialog(_id);
    }

    public override string ToString()
    {
        return _id;
    }
}
using Core.Http;
using Core.Session;
using Core.Session.Dialogs;
using Core.Session.Elements;

namespace Core;

public class WebDriverSession(string id)
{
    public SessionLocation Location = new(id);
    
    public IElementSelector Dom = new SessionElementSelector(id);
    
    public SessionContext Context = new(id);
    
    public ISimpleDialog Alert = new SimpleDialog(id);
    
    public ISimpleDialog Confirm = new SimpleDialog(id);
    
    public PromptDialog Prompt = new(id);
}
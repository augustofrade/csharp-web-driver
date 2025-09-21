using Core.Http;
using Core.Session;
using Core.Session.Dialogs;
using Core.Session.Elements;
using Core.Session.Windows;

namespace Core;

/// <summary>
/// Browser Session created by the Web Driver.
/// <br/>
/// WebDriverSession objects can be used to interact with its respective browser process and perform actions. 
/// </summary>
public class WebDriverSession
{
    /// <summary>
    /// ID of the session to be used by every session part 
    /// </summary>
    private readonly string _id;
    
    /// <summary>
    /// Location related operations in the Session.
    /// </summary>
    public SessionLocation Location;
    
    /// <summary>
    /// DOM interaction in the Session.
    /// </summary>
    public IElementSelector Dom;
    
    /// <summary>
    /// Browser Window Context related operations in the Session.
    /// </summary>
    public SessionContext Context;

    /// <summary>
    /// Window related operations in the Session.
    /// </summary>
    public SessionWindowManager Windows;
    
    /// <summary>
    /// Alert Dialog related operations in the Session.
    /// <br/>
    /// Generic implementation of ISimpleDialog with no difference of the one in the <see cref="Confirm"/>
    /// <br/>
    /// Exists as an option to describe that the dialog handling is related to Dialogs of the type "Alert".
    /// </summary>
    public ISimpleDialog Alert;
    
    /// <summary>
    /// Confirm Dialog related operations in the Session.
    /// <br/>
    /// Generic implementation of ISimpleDialog with no difference of the one in the <see cref="Alert"/>
    /// <br/>
    /// Exists as an option to describe that the dialog handling is related to Dialogs of the type "Confirm".
    /// </summary>
    public ISimpleDialog Confirm;
    
    /// <summary>
    /// Prompt related implementation of ISimpleDialog operations in the Session.
    /// </summary>
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
    
    /// <returns>ID of the WebDriver Session</returns>
    public override string ToString()
    {
        return _id;
    }
}
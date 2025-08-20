namespace Core.Session.Dialogs;

public interface ISimpleDialog
{
    Task Accept();
    Task Dismiss();
    Task<string> GetText();
}
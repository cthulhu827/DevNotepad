namespace DevNotepad.Infrastructure;

public interface IKeyHandler
{
    bool HandleKey(KeyEventArgs e)
    {
        return false;
    }

    IKeyHandler[] NestedKeyHandlers => Array.Empty<IKeyHandler>();
}
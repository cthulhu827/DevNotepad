namespace DevNotepad.Infrastructure;

public interface IKeyHandler
{
    bool HandleKey(KeyEventArgs e)
    {
        return false;
    }

    bool IsFocused()
    {
        return true;
    }

    IKeyHandler[] NestedKeyHandlers => Array.Empty<IKeyHandler>();

    bool HandleKeyWithNested(KeyEventArgs e)
    {
        if (!IsFocused()) return false;

        if (HandleKey(e)) return true;

        foreach (var nested in NestedKeyHandlers)
        {
            if (nested.HandleKeyWithNested(e)) return true;
        }

        return false;
    }
}
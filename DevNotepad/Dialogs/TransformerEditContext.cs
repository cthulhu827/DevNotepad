using DevNotepad.Core.Lists;

namespace DevNotepad.Dialogs;

public class TransformerEditContext
{
    private readonly Action changed;

    public TransformerEditContext(bool isNew, Selection? selection, IStringListsProvider listProvider,
        Guid listId, Action changed)
    {
        IsNew = isNew;
        Selection = selection;
        ListProvider = listProvider;
        ListId = listId;
        this.changed = changed;
    }

    public bool IsNew { get; }

    public Selection? Selection { get; }

    public IStringListsProvider ListProvider { get; }

    public Guid ListId { get; }

    public void Changed()
    {
        changed.Invoke();
    }
}
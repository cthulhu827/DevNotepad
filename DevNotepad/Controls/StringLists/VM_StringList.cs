using DevNotepad.Core.Lists;
using Framework.UI;

namespace DevNotepad.Controls.StringLists;

public class VM_StringList : ViewModel
{
    private static int GlobalId = 1;

    public VM_StringList(IStringList stringList, Guid currentListId)
    {
        Id = GlobalId++;

        Text = stringList.Caption;
        if (stringList.Id == currentListId) Text = $"{Text} (self)";

        ListId = stringList.Id;
        List = stringList;
    }

    public Guid ListId { get; }

    public IStringList List { get; }
}
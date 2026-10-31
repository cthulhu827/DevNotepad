using DevNotepad.Controls.StringLists;
using DevNotepad.Core.Lists;
using DevNotepad.Core.TextTransformers;
using DevNotepad.Dialogs.TPBase;
using Framework.MVC;

namespace DevNotepad.Dialogs.TPListDlg;

public class m_TPListDlg : m_TPBase<ListTransformer, p_TPListDlg>
{
    public override void Init(IParametrizedTextTransformer newTransformer, string[] newSource, TransformerEditContext newContext)
    {
        base.Init(newTransformer, newSource, newContext);

        if (context == null) return;

        var allLists = context.ListProvider.GetAll();
        var graph = new StringListGraph(allLists);
        var dependentListIds = graph.GetDependentLists(context.ListId);
        var listsToAdd = allLists.Where(l => !dependentListIds.Contains(l.Id)).ToArray();
        foreach (var stringList in listsToAdd)
        {
            var vm = new VM_StringList(stringList, context.ListId);
            DsList1.AddItem(vm);
            DsList2.AddItem(vm);
        }

        Transformer.CurrenListId = context.ListId;

        if (List1 == null && List2 == null)
        {
            List1 = listsToAdd.SingleOrDefault(l => l.Id == context.ListId);
            List2 = listsToAdd.FirstOrDefault(l => l.Id != context.ListId);
        }
    }

    public IDataSource<VM_StringList> DsList1 { get; } = DataSourceFactory.Create<VM_StringList>();

    public IDataSource<VM_StringList> DsList2 { get; } = DataSourceFactory.Create<VM_StringList>();

    public IStringList? List1
    {
        get => Transformer.List1;
        set => eventRaiser.Raise(() => Transformer.List1 = value, p_TPListDlg.List1Changed);
    }

    public IStringList? List2
    {
        get => Transformer.List2;
        set => eventRaiser.Raise(() => Transformer.List2 = value, p_TPListDlg.List2Changed);
    }

    public ListOperation Operation
    {
        get => Transformer.Operation;
        set => eventRaiser.Raise(() => Transformer.Operation = value, p_TPListDlg.OperationChanged);
    }
}
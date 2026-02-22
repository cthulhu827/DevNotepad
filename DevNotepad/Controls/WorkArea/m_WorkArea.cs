using DevNotepad.Controls.PipeControl;
using DevNotepad.Core.TextTransformers;
using DevNotepad.Dialogs;
using DevNotepad.Dialogs.TPBase;
using Framework.AppInfrastructure;
using Framework.MVC;
using System.Reflection;

namespace DevNotepad.Controls.WorkArea;

public class m_WorkArea : MVC_Model, ITransformerEditSession
{
    private readonly EventRaiser<p_WorkArea> eventRaiser;

    private string[] source = Array.Empty<string>();

    private int pipeIndex = -1;

    public m_WorkArea()
    {
        eventRaiser = new EventRaiser<p_WorkArea>(ApplyChanges);
    }

    public IDataSource<VM_PipeItem> Pipe { get; } = new DataSource<VM_PipeItem>();

    public int PipeIndex
    {
        get => pipeIndex;
        set
        {
            if (pipeIndex == value) return;
            eventRaiser.Raise(() => pipeIndex = value, p_WorkArea.PipeIndexChanged);
        }
    }

    public string[] Source
    {
        get => source;
        set
        {
            if (AreEqual(source, value)) return;
            eventRaiser.Raise(() => source = value, p_WorkArea.SourceChanged);
        }
    }

    public string SourceDescription => $"Count: {Source.Length}";

    public string[] Transformed { get; private set; } = Array.Empty<string>();

    public string TransformedDescription => $"Count: {Transformed.Length}";

    public void AddItem(ITextTransformer transformer)
    {
        var restorePipeIndex = pipeIndex;
        var newPipeItem = new VM_PipeItem(transformer);

        eventRaiser.Raise(() =>
        {
            Pipe.AddItem(newPipeItem);
            PipeIndex = Pipe.Count - 1;
        });

        EditNewItemIfNeed(newPipeItem, restorePipeIndex);
    }

    public void InsertItem(ITextTransformer transformer, bool before)
    {
        var restorePipeIndex = pipeIndex;
        var newPipeItem = new VM_PipeItem(transformer);

        eventRaiser.Raise(() =>
        {
            if (before)
            {
                Pipe.InsertBefore(Pipe[pipeIndex].Id, newPipeItem);
            }
            else
            {
                if (pipeIndex == Pipe.Count - 1)
                    Pipe.AddItem(newPipeItem);
                else
                    Pipe.InsertBefore(Pipe[pipeIndex + 1].Id, newPipeItem);
                PipeIndex++;
            }
        });

        EditNewItemIfNeed(newPipeItem, restorePipeIndex);
    }

    public void EditSelectedItem(bool shift)
    {
        if (pipeIndex == -1) return;

        var selectedItem = Pipe[pipeIndex];

        // Ctrl+Shift+Enter всегда разбирает композитный элемент
        if (shift)
        {
            DeconstructSelectedItem(selectedItem);
            return;
        }

        // Ctrl+Enter редактирует элемент, если он параметризованный,
        // или разбирает, если он композитный. Если он и параметризованный и композитный,
        // то редактирует (т.к. это проще отменить) - если пользователь хотел его разобрать,
        // то просто нажмёт Esc и следом Ctrl+Shift+Enter.
        if (selectedItem.Transformer is IParametrizedTextTransformer)
            EditSelectedItem(selectedItem);
        else if (selectedItem.Transformer is ICombinedTextTransformer)
            DeconstructSelectedItem(selectedItem);
    }

    public void DeleteSelectedItem()
    {
        if (pipeIndex == -1) return;

        var vm = Pipe[pipeIndex];
        eventRaiser.Raise(() =>
        {
            Pipe.RemoveItem(vm.Id);
            if (pipeIndex > Pipe.Count - 1) PipeIndex = Pipe.Count - 1;
        });
    }

    public override void StartListeningDomain()
    {
        base.StartListeningDomain();
        Pipe.MessageBus.Sign<NEListChanged>(On_PipeChanged);
    }

    public override void StopListeningDomain()
    {
        Pipe.MessageBus.UnsignObject(this);
        base.StopListeningDomain();
    }

    private bool UpdateTransformed()
    {
        var result = source;
        for (int i = 0; i <= pipeIndex; i++)
        {
            result = Pipe[i].Transformer.Transform(result);
        }

        var changed = !AreEqual(result, Transformed);
        if (changed) Transformed = result;
        return changed;
    }

    private void ApplyChanges(ICollection<p_WorkArea> changes)
    {
        var changed = UpdateTransformed();
        if (changed) changes.Add(p_WorkArea.TransformedChanged);

        NotifyChanged(changes.Select(change => (int)change).ToArray());
    }

    private void On_PipeChanged(NEListChanged evnt)
    {
        eventRaiser.Raise(() => { }, p_WorkArea.PipeChanged);
    }

    private static bool AreEqual(string[] a1, string[] a2)
    {
        if (a1.Length != a2.Length) return false;
        return !a1.Where((s, idx) => s != a2[idx]).Any();
    }

    private string[] GetSourceBeforeItem(int itemId)
    {
        var result = source;
        foreach (var pipeItem in Pipe.Items)
        {
            if (pipeItem.Id == itemId) break;
            result = pipeItem.Transformer.Transform(result);
        }

        return result;
    }

    private void EditSelectedItem(VM_PipeItem selectedItem)
    {
        if (selectedItem.Transformer is not IParametrizedTextTransformer parametrizedTransformer)
        {
            return;
        }

        var state = parametrizedTransformer.SaveState();
        EditItemIfNeed(selectedItem, () =>
        {
            // Если в диалоге нажали кнопку Отмена, то откатываем сделанные в диалоге изменения.
            eventRaiser.Raise(() => { parametrizedTransformer.RestoreState(state); });
        });
    }

    private void DeconstructSelectedItem(VM_PipeItem selectedItem)
    {
        if (selectedItem.Transformer is not ICombinedTextTransformer selectedTransformer) return;

        var transformers = selectedTransformer.Components;
        eventRaiser.Raise(() =>
        {
            Pipe.BeginUpdate();
            try
            {
                foreach (var tranformer in transformers)
                {
                    var vm = new VM_PipeItem(tranformer);
                    Pipe.InsertBefore(selectedItem.Id, vm);
                }

                Pipe.RemoveItem(selectedItem.Id);
                PipeIndex = PipeIndex + transformers.Length - 1;
            }
            finally
            {
                Pipe.EndUpdate();
            }
        });
    }

    private void EditNewItemIfNeed(VM_PipeItem newPipeItem, int restorePipeIndex)
    {
        EditItemIfNeed(newPipeItem, () =>
        {
            // Если в диалоге нажали кнопку Отмена, то удаляем добавленный трансформер.
            eventRaiser.Raise(() =>
            {
                Pipe.RemoveItem(newPipeItem.Id);
                PipeIndex = restorePipeIndex;
            });
        });
    }

    private void EditItemIfNeed(VM_PipeItem newPipeItem, Action restore)
    {
        if (newPipeItem.Transformer is not IParametrizedTextTransformer parmetrizedTransformer)
        {
            return;
        }

        var controllerType = AnnotatedClasses
            .GetTypesWith<TPEditorAttribute>()
            .Select(t => new { Attr = t.GetCustomAttribute<TPEditorAttribute>(), ControllerType = t })
            .SingleOrDefault(pair => pair.Attr!.TransformerType == newPipeItem.Transformer.GetType())
            ?.ControllerType;
        if (controllerType == null)
        {
            return;
        }

        var sourceBefore = GetSourceBeforeItem(newPipeItem.Id);
        newPipeItem.Transformer.Transform(sourceBefore);

        var controller = (ITPDlgController)Activator.CreateInstance(controllerType)!;
        if (!controller.Edit(parmetrizedTransformer, sourceBefore, this))
        {
            // При нажатии ОК в диалоге ничего дополнительно обновлять не нужно,
            // т.к. все изменения применялись немедленно при редактировании диалога.
            // Если в диалоге нажали кнопку Отмена, то нужно восстановить
            // состояние до начала редактирования.
            restore.Invoke();
        }
    }

    #region ITransformerEditSession

    public void Changed()
    {
        ApplyChanges(new List<p_WorkArea>());
    }

    #endregion
}
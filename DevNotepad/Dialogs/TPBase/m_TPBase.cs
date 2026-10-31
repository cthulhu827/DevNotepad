using DevNotepad.Core.TextTransformers;
using Framework.MVC;

namespace DevNotepad.Dialogs.TPBase;

public class m_TPBase<TTransformer, TEnum> : MVC_Model, ITPDlgModel
    where TTransformer : ITextTransformer, IParametrizedTextTransformer
    where TEnum : struct, Enum
{
    private TTransformer? transformer;
    protected string[]? source;
    protected TransformerEditContext? context;

    protected readonly EventRaiser<TEnum> eventRaiser;

    public m_TPBase()
    {
        eventRaiser = new EventRaiser<TEnum>(ApplyChanges);
    }

    public virtual void Init(IParametrizedTextTransformer newTransformer, string[] newSource, TransformerEditContext newContext)
    {
        transformer = (TTransformer)newTransformer;
        source = newSource;
        context = newContext;
    }

    public TTransformer Transformer => transformer ?? throw new Exception("Method Init() hasn't been called");

    ITextTransformer ITPDlgModel.Transformer => Transformer;

    private string[] Source => source ?? throw new Exception("Method Init() hasn't been called");

    private void ApplyChanges(ICollection<TEnum> changes)
    {
        Transformer.Transform(Source);
        NotifyChanged(changes.Select(change => Convert.ToInt32(change)).ToArray());
        context?.Changed();
    }
}
using DevNotepad.Core.TextTransformers;
using Framework.MVC;

namespace DevNotepad.Dialogs.TPBase;

public class m_TPBase<TTransformer, TEnum> : MVC_Model, ITPDlgModel
    where TTransformer : ITextTransformer, IParametrizedTextTransformer
    where TEnum : struct, Enum
{
    private TTransformer? transformer;
    private string[]? source;
    private ITransformerEditSession? editSession;

    protected readonly EventRaiser<TEnum> eventRaiser;

    public m_TPBase()
    {
        eventRaiser = new EventRaiser<TEnum>(ApplyChanges);
    }

    public void Init(IParametrizedTextTransformer newTransformer, string[] newSource, ITransformerEditSession newEditSession)
    {
        transformer = (TTransformer)newTransformer;
        source = newSource;
        editSession = newEditSession;
    }

    public TTransformer Transformer => transformer ?? throw new Exception("Method Init() hasn't been called");

    ITextTransformer ITPDlgModel.Transformer => Transformer;

    private string[] Source => source ?? throw new Exception("Method Init() hasn't been called");

    private ITransformerEditSession EditSession => editSession ?? throw new Exception("Method Init() hasn't been called");

    private void ApplyChanges(ICollection<TEnum> changes)
    {
        Transformer.Transform(Source);
        NotifyChanged(changes.Select(change => Convert.ToInt32(change)).ToArray());
        EditSession.Changed();
    }
}
using DevNotepad.Core.TextTransformers;
using Framework.UI;

namespace DevNotepad.Controls.PipeControl;

public class VM_PipeItem : ViewModel
{
    private static int GlobalId = 1;

    public VM_PipeItem(ITextTransformer transformer)
    {
        Id = GlobalId++;
        Text = transformer.Caption;
        Transformer = transformer;
    }

    public ITextTransformer Transformer { get; }
}
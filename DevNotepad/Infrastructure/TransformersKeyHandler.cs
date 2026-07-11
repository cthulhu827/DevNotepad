using DevNotepad.Core;
using DevNotepad.Core.TextTransformers;

namespace DevNotepad.Infrastructure;

public class TransformersKeyHandler : IKeyHandler
{
    private readonly Action<ITextTransformer> transformerHandler;
    private readonly TransformerInfo[] transformersWithShortucts;

    public TransformersKeyHandler(Action<ITextTransformer> transformerHandler)
    {
        this.transformerHandler = transformerHandler;
        transformersWithShortucts = Domain.All.Where(ti => ti.ShortCut != 0).ToArray();
    }

    public bool HandleKey(KeyEventArgs e)
    {
        foreach (var transformerInfo in transformersWithShortucts)
        {
            if (transformerInfo.ShortCut != (int)e.KeyData) continue;

            var transformer = transformerInfo.Build();
            transformerHandler.Invoke(transformer);
            return true;
        }

        return false;
    }
}
namespace DevNotepad.Core.TextTransformers
{
    public interface IParametrizedTextTransformer
    {
        object SaveState();
        void RestoreState(object state);
    }
}
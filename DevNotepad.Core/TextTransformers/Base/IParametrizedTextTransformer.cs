namespace DevNotepad.Core.TextTransformers
{
    public interface IParametrizedTextTransformer
    {
        // Можно использовать в случаях, когда все нужные параметры задаются билдером,
        // поэтому окно редактирования при создании будет лишним.
        bool DontEditNew => false;
    }
}
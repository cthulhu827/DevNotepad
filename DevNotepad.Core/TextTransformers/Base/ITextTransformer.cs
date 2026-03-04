using System;

namespace DevNotepad.Core.TextTransformers
{
    public interface ITextTransformer
    {
        Guid Id { get; }
        string Caption { get; }

        void Init(Guid id, string caption);

        string[] Transform(string[] lines);
    }
}
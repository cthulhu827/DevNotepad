using Newtonsoft.Json;
using System;

namespace DevNotepad.Core.TextTransformers
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class BaseTextTransformer : ITextTransformer
    {
        public Guid Id { get; private set; } = Guid.Empty;
        public string Caption { get; private set; } = "";

        public void Init(Guid id, string caption)
        {
            Id = id;
            Caption = caption;
        }

        public abstract string[] Transform(string[] lines);
    }
}
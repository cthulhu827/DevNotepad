using System;

namespace DevNotepad.Core.TextTransformers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class TextTransformerAttribute : Attribute
    {
        public TextTransformerAttribute(string caption, string id)
        {
            Id = Guid.Parse(id);
            Caption = caption;
        }

        public Guid Id { get; }

        public string Caption { get; }
    }
}
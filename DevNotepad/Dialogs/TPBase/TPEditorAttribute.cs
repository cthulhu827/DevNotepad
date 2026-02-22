namespace DevNotepad.Dialogs.TPBase;

[AttributeUsage(AttributeTargets.Class)]
public class TPEditorAttribute : Attribute
{
    public TPEditorAttribute(Type transformerType)
    {
        TransformerType = transformerType;
    }

    public Type TransformerType { get; }
}
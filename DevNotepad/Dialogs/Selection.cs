namespace DevNotepad.Dialogs;

public class Selection
{
    public Selection(int start, int length, string text, string line)
    {
        Start = start;
        Length = length;
        Text = text;
        Line = line;
    }

    public int Start { get; }
    public int Length { get; }
    public string Text { get; }
    public string Line { get; }
}
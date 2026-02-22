namespace DevNotepad.Controls.PipeControl;

public class PipeInsertItemEventArgs : EventArgs
{
    public PipeInsertItemEventArgs(bool before)
    {
        Before = before;
    }

    public bool Before { get; }
}

public class PipeEditItemEventArgs : EventArgs
{
    public PipeEditItemEventArgs(bool shift)
    {
        Shift = shift;
    }

    public bool Shift { get; }
}
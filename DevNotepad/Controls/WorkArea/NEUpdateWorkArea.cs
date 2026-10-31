using Framework.AppInfrastructure;

namespace DevNotepad.Controls.WorkArea;

public class NEUpdateWorkArea : NotifyEvent
{
    public NEUpdateWorkArea(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}
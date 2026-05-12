using DevNotepad.Controls.WorkArea;
using Framework.MVC;

namespace DevNotepad.Controls.Page;

public class m_Page : MVC_Model
{
    private readonly EventRaiser<p_Page> eventRaiser;

    private readonly IList<m_WorkArea> workAreas = new List<m_WorkArea>();

    public m_Page()
    {
        eventRaiser = new EventRaiser<p_Page>(ApplyChanges);
        AddWorkArea();
    }

    public m_WorkArea[] WorkAreas => workAreas.ToArray();

    public void AddWorkArea(m_WorkArea? model = null)
    {
        eventRaiser.Raise(() => workAreas.Add(model ?? new m_WorkArea()), p_Page.WorkAreasListChanged);
    }

    public void RemoveWorkArea(int idx)
    {
        if (idx == -1) return;
        eventRaiser.Raise(() => workAreas.RemoveAt(idx), p_Page.WorkAreasListChanged);
    }

    private void ApplyChanges(ICollection<p_Page> changes)
    {
        NotifyChanged(changes.Select(change => (int)change).ToArray());
    }
}
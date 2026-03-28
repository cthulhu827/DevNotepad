using DevNotepad.Controls.WorkArea;
using DevNotepad.Infrastructure;
using Framework.MVC;

namespace DevNotepad.Controls.Page;

public class c_Page : MVC_Controller<m_Page, v_Page>, IKeyHandler
{
    private readonly IList<c_WorkArea> workAreas = new List<c_WorkArea>();

    protected override void DoConnectModel()
    {
        base.DoConnectModel();

        ApplyModelChanges(null);
    }

    protected override void OnModelChanged(int[] changeCodes)
    {
        ApplyModelChanges(changeCodes.ToEnums<p_Page>());
    }

    private void ApplyModelChanges(p_Page[]? changes)
    {
        changes ??= Enums.Values<p_Page>();

        modelSuppressor.Exec(() =>
        {
            if (changes.Contains(p_Page.WorkAreaAdded))
            {
                // todo: учитывать, что может быть добавлено несколько моделей; есть пример в c_MainWindow
                if (Model.WorkAreas.Any()) AddWorkArea(Model.WorkAreas.Last());
            }
        });
    }

    private void AddWorkArea(m_WorkArea model)
    {
        var controller = new c_WorkArea
        {
            View = new v_WorkArea(),
            Model = model
        };
        workAreas.Add(controller);
        AddWorkArea(controller);
    }

    private void AddWorkArea(c_WorkArea controller)
    {
        var prevView = workAreas
            .Select(c => c.View)
            .Reverse()
            .Skip(1)
            .FirstOrDefault();
        if (prevView != null) prevView.Dock = DockStyle.Top;

        var view = controller.View;
        view.Parent = View;
        view.Dock = DockStyle.Fill;
        view.BringToFront();

        AdjusthWorkAreas();

        controller.SetFocus();
    }

    private void AdjusthWorkAreas()
    {
        var allViews = workAreas.Select(controller => controller.View).ToArray();
        var unresizedViews = allViews.Where(view => !view.ManualResized).ToArray();
        if (unresizedViews.Length < 2) return;

        var resizedViewsHeight = allViews.Where(view => view.ManualResized).Sum(view => view.Height);
        var unresizedViewHeight = (View.ClientSize.Height - resizedViewsHeight) / unresizedViews.Length;
        foreach (var view in unresizedViews.Take(unresizedViews.Length - 1))
        {
            view.Height = unresizedViewHeight;
        }
    }

    private int GetFocusedWorkAreaIdx()
    {
        for (int i = 0; i < workAreas.Count; i++)
            if (workAreas[i].View.ContainsFocus)
                return i;
        return -1;
    }

    private void FocusWorkArea(int idx, int focusedIdx)
    {
        if (idx >= 0 && idx < workAreas.Count && idx != focusedIdx)
            workAreas[idx].SetFocus();
    }

    public void SetFocus()
    {
        workAreas.LastOrDefault()?.SetFocus();
    }

    #region IKeyHandler implementation

    public bool HandleKey(KeyEventArgs e)
    {
        if (!View.ContainsFocus) return false;

        var result = true;
        if (e is { KeyCode: Keys.N, Control: true })
        {
            var activeWorkArea = workAreas.FirstOrDefault(c => c.View.ContainsFocus);
            var newModel = activeWorkArea?.ModelNullable?.Copy(e.Shift, e.Alt);
            Model.AddWorkArea(newModel);
        }
        else if (e is { KeyCode: Keys.Down, Control: true })
        {
            var focusedIdx = GetFocusedWorkAreaIdx();
            FocusWorkArea(focusedIdx + 1, focusedIdx);
        }
        else if (e is { KeyCode: Keys.Up, Control: true })
        {
            var focusedIdx = GetFocusedWorkAreaIdx();
            FocusWorkArea(focusedIdx - 1, focusedIdx);
        }
        else if (e is { KeyCode: >= Keys.NumPad1 and <= Keys.NumPad9, Control: true })
            FocusWorkArea(e.KeyCode - Keys.NumPad1, GetFocusedWorkAreaIdx());
        else
            result = false;

        return result;
    }

    public IKeyHandler[] NestedKeyHandlers => workAreas.Cast<IKeyHandler>().ToArray();

    #endregion
}
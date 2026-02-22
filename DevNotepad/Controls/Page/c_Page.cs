using DevNotepad.Controls.WorkArea;
using Framework.MVC;

namespace DevNotepad.Controls.Page;

public class c_Page : MVC_Controller<m_Page, v_Page>
{
    public readonly IList<c_WorkArea> workAreas = new List<c_WorkArea>();

    protected override void DoConnectModel()
    {
        base.DoConnectModel();

        ApplyModelChanges(null);
    }

    protected override void DoDisconnectModel()
    {
        base.DoDisconnectModel();
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
                // todo: учитывать, что может быть добавлено несколько моделей
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
        AddWorkArea(controller.View);
    }

    private void AddWorkArea(v_WorkArea view)
    {
        var prevView = workAreas
            .Select(controller => controller.View)
            .Reverse()
            .Skip(1)
            .FirstOrDefault();
        if (prevView != null) prevView.Dock = DockStyle.Top;

        view.Parent = View;
        view.Dock = DockStyle.Fill;
        view.BringToFront();

        AdjusthWorkAreas();

        view.txtSource.Focus();
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
}
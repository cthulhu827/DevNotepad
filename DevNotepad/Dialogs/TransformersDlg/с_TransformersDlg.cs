using Framework.MVC;
using Framework.Domain;

namespace DevNotepad.Dialogs.TransformersDlg;

public class с_TransformersDlg : ModalDialogController<m_TransformersDlg, v_TransformersDlg>
{
    protected override void DoConnectModel()
    {
        base.DoConnectModel();

        View.txtSearch.TextChanged += txtSearch_TextChanged;
        View.txtSearch.KeyDown += txtSearch_KeyDown;
        View.lbTransformers.SelectedIndexChanged += lbTransformers_SelectedIndexChanged;
        View.lbTransformers.DrawItem += lbTransformers_DrawItem;

        View.lbTransformers.DataSource = Model.FilteredTransformers;
        ApplyModelChanges(null);
    }

    private void lbTransformers_DrawItem(object? sender, DrawItemEventArgs e)
    {
        var vm = Model.FilteredTransformers[e.Index];
        var g = e.Graphics;
        var b = e.State.HasFlag(DrawItemState.Selected) ? UI.BrChatListSel : UI.BrChatListBg;
        g.FillRectangle(b, e.Bounds);
        g.DrawString(vm.Text, UI.Font14, UI.BrFont, new PointF(e.Bounds.X, e.Bounds.Y));
    }

    private void txtSearch_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Down)
        {
            View.SelectNextItem();
            e.Handled = true;
        }
        else if (e.KeyCode == Keys.Up)
        {
            View.SelectPrevItem();
            e.Handled = true;
        }
    }

    private void lbTransformers_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (modelSuppressor.Suppress) return;
        Model.SelectedId = View.lbTransformers.Selection.SingleId;
    }

    private void txtSearch_TextChanged(object? sender, EventArgs e)
    {
        if (modelSuppressor.Suppress) return;
        Model.SearchText = View.txtSearch.Text;
    }

    protected override void DoDisconnectModel()
    {
        View.lbTransformers.DataSource = null;

        View.txtSearch.TextChanged -= txtSearch_TextChanged;
        View.txtSearch.KeyDown -= txtSearch_KeyDown;
        View.lbTransformers.SelectedIndexChanged -= lbTransformers_SelectedIndexChanged;
        View.lbTransformers.DrawItem -= lbTransformers_DrawItem;

        base.DoDisconnectModel();
    }

    protected override void OnModelChanged(int[] changeCodes)
    {
        ApplyModelChanges(changeCodes);
    }

    private void ApplyModelChanges(int[]? changes)
    {
        changes ??= m_TransformersDlg.AllChanges;

        modelSuppressor.Exec(() =>
        {
            if (changes.Contains(m_TransformersDlg.SearchTextChanged))
                View.txtSearch.Text = Model.SearchText;
            if (changes.Contains(m_TransformersDlg.SelectionChanged))
            {
                View.lbTransformers.SetSelectedId(Model.SelectedId);
                View.btnOK.Enabled = Model.SelectedId != Entity.NullId;
            }
        });
    }
}
using Framework.MVC;
using Framework.Domain;
using Framework.UI;

namespace DevNotepad.Dialogs.SearchListDlg;

public class c_SearchListDlg<TModel, TView, TViewModel> : ModalDialogController<TModel, TView>
    where TModel : m_SearchListDlg<TViewModel>
    where TView : ModalDialog, ISearchListDlg<TViewModel>
    where TViewModel : ViewModel
{
    protected override void DoConnectModel()
    {
        base.DoConnectModel();

        View.SearchTextBox.TextChanged += txtSearch_TextChanged;
        View.SearchTextBox.KeyDown += txtSearch_KeyDown;
        View.ListBox.SelectedIndexChanged += ListBox_SelectedIndexChanged;
        View.ListBox.DrawItem += ListBox_DrawItem;

        View.ListBox.DataSource = Model.Filtered;
        ApplyModelChanges(null);
    }

    protected virtual void ListBox_DrawItem(object? sender, DrawItemEventArgs e)
    {
        var vm = Model.Filtered[e.Index];
        var g = e.Graphics;
        var brush = e.State.HasFlag(DrawItemState.Selected) ? UI.BrChatListSel : UI.BrChatListBg;
        g.FillRectangle(brush, e.Bounds);
        g.DrawString(vm.Text, UI.Font14, UI.BrFont, new PointF(e.Bounds.X, e.Bounds.Y));
    }

    private void txtSearch_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Down)
        {
            SelectNextItem();
            e.Handled = true;
        }
        else if (e.KeyCode == Keys.Up)
        {
            SelectPrevItem();
            e.Handled = true;
        }
    }

    private void ListBox_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (modelSuppressor.Suppress) return;
        Model.SelectedId = View.ListBox.Selection.SingleId;
    }

    private void txtSearch_TextChanged(object? sender, EventArgs e)
    {
        if (modelSuppressor.Suppress) return;
        Model.SearchText = View.SearchTextBox.Text;
    }

    protected override void DoDisconnectModel()
    {
        View.ListBox.DataSource = null;

        View.SearchTextBox.TextChanged -= txtSearch_TextChanged;
        View.SearchTextBox.KeyDown -= txtSearch_KeyDown;
        View.ListBox.SelectedIndexChanged -= ListBox_SelectedIndexChanged;
        View.ListBox.DrawItem -= ListBox_DrawItem;

        base.DoDisconnectModel();
    }

    protected override void OnModelChanged(int[] changeCodes)
    {
        ApplyModelChanges(changeCodes);
    }

    private void ApplyModelChanges(int[]? changes)
    {
        changes ??= m_SearchListDlg<TViewModel>.AllChanges;

        modelSuppressor.Exec(() =>
        {
            if (changes.Contains(m_SearchListDlg<TViewModel>.SearchTextChanged))
                View.SearchTextBox.Text = Model.SearchText;
            if (changes.Contains(m_SearchListDlg<TViewModel>.SelectionChanged))
            {
                View.ListBox.Selection.SetSelectedId(Model.SelectedId);
                View.btnOK.Enabled = Model.SelectedId != Entity.NullId;
            }
        });
    }

    private void SelectNextItem()
    {
        View.ListBox.SelectedIndex = View.ListBox.Items.Count == 0
            ? -1
            : Math.Min(View.ListBox.SelectedIndex + 1, View.ListBox.Items.Count - 1);
    }

    private void SelectPrevItem()
    {
        View.ListBox.SelectedIndex = View.ListBox.Items.Count == 0
            ? -1
            : Math.Max(View.ListBox.SelectedIndex - 1, 0);
    }
}
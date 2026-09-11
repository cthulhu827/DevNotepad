using DevNotepad.Dialogs.SearchListDlg;

namespace DevNotepad.Dialogs.TransformersDlg;

public class с_TransformersDlg : c_SearchListDlg<m_TransformersDlg, v_TransformersDlg, VM_TransformerForDlg>
{
    protected override void ListBox_DrawItem(object? sender, DrawItemEventArgs e)
    {
        base.ListBox_DrawItem(sender, e);
        var vm = Model.Filtered[e.Index];
        DrawDescription(vm, e.Graphics, e.Bounds);
        DrawShortCut(vm, e.Graphics, e.Bounds);
    }

    private static void DrawDescription(VM_TransformerForDlg viewModel, Graphics g, Rectangle itemBounds)
    {
        if (string.IsNullOrEmpty(viewModel.Description)) return;

        const int offset = 200;
        g.DrawString(viewModel.Description, UI.Font14, UI.BrFolderIcon, new PointF(itemBounds.X + offset, itemBounds.Y));
    }

    private static void DrawShortCut(VM_TransformerForDlg viewModel, Graphics g, Rectangle itemBounds)
    {
        if (string.IsNullOrEmpty(viewModel.ShortCut)) return;

        var shortcutRect = new RectangleF(itemBounds.Left, itemBounds.Y, itemBounds.Width - 2, itemBounds.Height);
        var format = new StringFormat { Alignment = StringAlignment.Far };
        g.DrawString(viewModel.ShortCut, UI.FontMono14, UI.BrFont, shortcutRect, format);
    }
}
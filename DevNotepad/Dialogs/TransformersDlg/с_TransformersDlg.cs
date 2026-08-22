using DevNotepad.Dialogs.SearchListDlg;

namespace DevNotepad.Dialogs.TransformersDlg;

public class с_TransformersDlg : c_SearchListDlg<m_TransformersDlg, v_TransformersDlg, VM_TransformerForDlg>
{
    protected override void ListBox_DrawItem(object? sender, DrawItemEventArgs e)
    {
        base.ListBox_DrawItem(sender, e);
        DrawShortCut(Model.Filtered[e.Index], e.Graphics, e.Bounds);
    }

    private static void DrawShortCut(VM_TransformerForDlg viewModel, Graphics g, Rectangle itemBounds)
    {
        if (string.IsNullOrEmpty(viewModel.ShortCut)) return;

        var shortcutRect = new RectangleF(itemBounds.Left, itemBounds.Y, itemBounds.Width - 2, itemBounds.Height);
        var format = new StringFormat { Alignment = StringAlignment.Far };
        g.DrawString(viewModel.ShortCut, UI.FontMono14, UI.BrFont, shortcutRect, format);
    }
}
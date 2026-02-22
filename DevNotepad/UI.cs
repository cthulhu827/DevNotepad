using DevNotepad.Core;
using DevNotepad.Dialogs.TransformersDlg;

namespace DevNotepad;

public static class UI
{
    public static readonly Color ClrBack = Color.FromArgb(14, 22, 33);

    public static readonly Color ClrListBack = Color.FromArgb(23, 33, 43);
    public static readonly Color ClrListSel = Color.FromArgb(43, 82, 120);
    public static readonly Color ClrListSel2 = Color.FromArgb(88, 170, 244);
    public static readonly Color ClrEditBack = Color.FromArgb(36, 47, 61);
    //public static readonly Color ClrEditForeDisabled = Color.FromArgb(98, 120, 109);
    public static readonly Color ClrEditForeDisabled = Color.FromArgb(118, 140, 158);
    public static readonly Color ClrListFore = Color.White;
    public static readonly Color ClrPipeBack = Color.FromArgb(37, 48, 62); // Folder list selection

    public static readonly Brush BrListBack = new SolidBrush(ClrListBack);
    public static readonly Brush BrListSel = new SolidBrush(ClrListSel);
    public static readonly Brush BrListFore = new SolidBrush(ClrListFore);
    public static readonly Brush BrWorkAreaHorzSplitter = new SolidBrush(ClrEditForeDisabled);
    public static readonly Brush BrWorkAreaVertSplitter = new SolidBrush(ClrPipeBack);

    public static readonly Font Font14 = new("Segoe UI", 14);
    public static readonly Font Font10 = new("Segoe UI", 10);
    public static readonly Font FontMono12 = new("Consolas", 12);

    public static Guid? AskTransformer()
    {
        var allTransformers = Domain.All
            .Select(a => new VM_TransformerForDlg(a.Id, a.Caption))
            .ToArray();

        var model = new m_TransformersDlg(allTransformers.ToArray());

        var controller = new с_TransformersDlg();
        if (!controller.ShowDialog(model)) return null;

        var selected = model.AllTransformers.SingleOrDefault(vm => vm.Id == model.SelectedId);
        return selected?.TransformerId;
    }

    public static void UnfocusCheckBox<T>(TextBox textBoxToFocus, T[] allChanges, params T[] changesToCheck)
    {
        if (allChanges.Intersect(changesToCheck).IsEmpty()) return;

        textBoxToFocus.Focus();
        textBoxToFocus.SelectionStart = textBoxToFocus.Text.Length;
    }
}
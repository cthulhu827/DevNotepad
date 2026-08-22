using DevNotepad.Core;
using DevNotepad.Dialogs.SavedPagesDlg;
using DevNotepad.Dialogs.TransformersDlg;
using DevNotepad.Infrastructure;

namespace DevNotepad;

public static class UI
{
    public static readonly Color ClrFont = Color.White;
    public static readonly Color ClrChatBg = Color.FromArgb(14, 22, 33);
    public static readonly Color ClrChatListBg = Color.FromArgb(23, 33, 43);
    public static readonly Color ClrChatListSel = Color.FromArgb(43, 82, 120);
    public static readonly Color ClrFolderListSel = Color.FromArgb(37, 48, 62);
    public static readonly Color ClrFolderIcon = Color.FromArgb(118, 140, 158);
    public static readonly Color ClrFolderIconSel = Color.FromArgb(94, 181, 247);

    public static readonly Brush BrFont = new SolidBrush(ClrFont);
    public static readonly Brush BrChatBg = new SolidBrush(ClrChatBg);
    public static readonly Brush BrChatListBg = new SolidBrush(ClrChatListBg);
    public static readonly Brush BrChatListSel = new SolidBrush(ClrChatListSel);
    public static readonly Brush BrFolderListSel = new SolidBrush(ClrFolderListSel);

    public static readonly Font Font14 = new("Segoe UI", 14);
    public static readonly Font Font10 = new("Segoe UI", 10);
    public static readonly Font FontMono12 = new("Consolas", 12);
    public static readonly Font FontMono14 = new("Consolas", 14);

    public static Guid? AskTransformer()
    {
        var counter = new TransformersCallCounter(Program.Settings.RecentTransformersPath);
        var counts = counter.ReadAll();
        var allTransformers = Domain.All
            .Select(a => new VM_TransformerForDlg(a.Id, a.Caption, a.ShortCutStr))
            .OrderByDescending(a => counts.TryGetValue(a.TransformerId, out var c) ? c : 0)
            .ThenBy(a => a.Caption, StringComparer.CurrentCultureIgnoreCase)
            .ToArray();

        var model = new m_TransformersDlg(allTransformers.ToArray());

        var controller = new с_TransformersDlg();
        if (!controller.ShowDialog(model)) return null;

        var selected = model.All.SingleOrDefault(vm => vm.Id == model.SelectedId);
        if (selected != null) counter.Increment(selected.TransformerId);
        return selected?.TransformerId;
    }

    public static string? SelectSavedPage()
    {
        var model = new m_SavedPagesDlg();

        var controller = new c_SavedPagesDlg();
        if (!controller.ShowDialog(model)) return null;

        var selected = model.All.SingleOrDefault(vm => vm.Id == model.SelectedId);
        return selected?.FileName;
    }

    public static void UnfocusCheckBox<T>(TextBox textBoxToFocus, T[] allChanges, params T[] changesToCheck)
    {
        if (allChanges.Intersect(changesToCheck).IsEmpty()) return;

        textBoxToFocus.Focus();
        textBoxToFocus.SelectionStart = textBoxToFocus.Text.Length;
    }
}
using System.Runtime.InteropServices;
using Framework.UI;

namespace DevNotepad.Controls.StringLists;

public class lb_StringList : DataSourceListBox<VM_StringList>
{
    /// <summary>Цвет рамки, когда список в фокусе.</summary>
    public Color FocusedBorderColor { get; set; } = UI.ClrFolderIconSel;

    /// <summary>Цвет рамки, когда список не в фокусе.</summary>
    public Color UnfocusedBorderColor { get; set; } = UI.ClrFolderListSel;

    protected override void WndProc(ref Message m)
    {
        base.WndProc(ref m);

        if (m.Msg == WM_NCPAINT)
        {
            DrawCustomBorder();
        }
    }

    protected override void OnEnter(EventArgs e)
    {
        base.OnEnter(e);
        RedrawFrame();
    }

    protected override void OnLeave(EventArgs e)
    {
        base.OnLeave(e);
        RedrawFrame();
    }

    private void RedrawFrame()
    {
        RedrawWindow(Handle, IntPtr.Zero, IntPtr.Zero, RDW_FRAME | RDW_INVALIDATE | RDW_UPDATENOW);
    }

    private void DrawCustomBorder()
    {
        var hdc = GetWindowDC(Handle);
        if (hdc == IntPtr.Zero)
        {
            return;
        }

        try
        {
            using var g = Graphics.FromHdc(hdc);
            using var pen = new Pen(Focused ? FocusedBorderColor : UnfocusedBorderColor);
            g.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
        }
        finally
        {
            ReleaseDC(Handle, hdc);
        }
    }

    #region Win API

    private const int WM_NCPAINT = 0x0085;

    private const int RDW_INVALIDATE = 0x0001;
    private const int RDW_FRAME = 0x0400;
    private const int RDW_UPDATENOW = 0x0100;

    [DllImport("user32.dll")]
    private static extern IntPtr GetWindowDC(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

    [DllImport("user32.dll")]
    private static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, int flags);

    #endregion
}
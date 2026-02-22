using System.Runtime.InteropServices;

namespace DevNotepad.Controls;

public class ScrollableTextBox : TextBox
{
    // ReSharper disable once InconsistentNaming
    private const int EM_LINESCROLL = 0x00B6;

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

    protected override void OnMouseWheel(MouseEventArgs e)
    {
        base.OnMouseWheel(e);

        var wheelNotches = e.Delta / 120;
        if (wheelNotches == 0)
            return;

        var verticalLines = SystemInformation.MouseWheelScrollLines;

        if ((ModifierKeys & Keys.Shift) == Keys.Shift)
        {
            var columns = Math.Clamp(verticalLines, 1, 20);
            SendMessage(Handle, EM_LINESCROLL, new IntPtr(-wheelNotches * columns), IntPtr.Zero);
        }
        else
        {
            SendMessage(Handle, EM_LINESCROLL, IntPtr.Zero, new IntPtr(-wheelNotches * verticalLines));
        }

        if (e is HandledMouseEventArgs h)
            h.Handled = true;
    }
}
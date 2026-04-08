using DevNotepad.Controls.PipeControl;
using System.ComponentModel;

namespace DevNotepad.Controls.WorkArea
{
    public partial class v_WorkArea : UserControl
    {
        private const int ResizeFrameHeight = 3;
        private const int SplitterWidth = 2;

        private bool isResizing;
        private int resizeStartPos;
        private int resizeStartHeight;
        private IMessageFilter? cancelByEscHandler;

        public v_WorkArea()
        {
            InitializeComponent();
        }

        private bool Resizable => Dock != DockStyle.Fill;

        public bool ManualResized { get; private set; }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            var vertSplitterRect = new Rectangle(txtSource.Width, 0, SplitterWidth, ClientSize.Height);
            e.Graphics.FillRectangle(UI.BrFolderListSel, vertSplitterRect);

            if (Resizable)
            {
                var horzSplitterRect = new Rectangle(0, ClientSize.Height - ResizeFrameHeight,
                    ClientSize.Width, ResizeFrameHeight);
                e.Graphics.FillRectangle(UI.BrChatBg, horzSplitterRect);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (!CantResize() && e.Button == MouseButtons.Left && IsInResizeZone(e.Location))
                StartResize(e.Location);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (CantResize())
            {
                return;
            }

            if (isResizing)
            {
                var delta = PointToScreen(e.Location).Y - resizeStartPos;
                var newHeight = resizeStartHeight + delta;

                if (MinimumSize.Height > 0)
                {
                    if (newHeight < MinimumSize.Height) newHeight = MinimumSize.Height;
                    if (newHeight > MaximumSize.Height) newHeight = MaximumSize.Height;
                }

                Height = newHeight;
                return;
            }

            if (IsInResizeZone(e.Location))
                EnsureResizeCursor();
            else
                RestoreCursorIfNeeded();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (CantResize() || !isResizing || e.Button != MouseButtons.Left)
            {
                return;
            }

            EndResize(false);

            if (IsInResizeZone(e.Location))
                EnsureResizeCursor();
            else
                RestoreCursorIfNeeded();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (!isResizing) RestoreCursorIfNeeded();
        }

        private void AdjustControls()
        {
            var width = (Width - SplitterWidth) / 2;

            var clientHeight = ClientSize.Height;
            if (Resizable) clientHeight -= ResizeFrameHeight;

            txtSource.SetBounds(0, pipeControl.Height,
                width, clientHeight - pipeControl.Height - lblSource.Height);
            txtTransformed.SetBounds(width + SplitterWidth, pipeControl.Height,
                Width - width, clientHeight - pipeControl.Height - lblTransformed.Height);

            lblSource.SetBounds(0, clientHeight - lblSource.Height,
                width, lblSource.Height);
            lblTransformed.SetBounds(width + SplitterWidth, clientHeight - lblTransformed.Height,
                Width - width, lblTransformed.Height);
        }

        private bool IsInResizeZone(Point clientPoint)
        {
            return clientPoint.Y >= ClientSize.Height - ResizeFrameHeight;
        }

        private void EnsureResizeCursor()
        {
            if (Cursor != Cursors.SizeNS) Cursor = Cursors.SizeNS;
        }

        private void RestoreCursorIfNeeded()
        {
            if (Cursor != Cursors.Default) Cursor = Cursors.Default;
        }

        private bool CantResize()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode || !Resizable;
        }

        private void StartResize(Point mouseLocationClient)
        {
            isResizing = true;
            resizeStartPos = PointToScreen(mouseLocationClient).Y;
            resizeStartHeight = Height;
            Capture = true;

            EnsureResizeCursor();
            InstallEscapeCancelFilter();
        }

        private void EndResize(bool cancel)
        {
            if (!isResizing)
            {
                return;
            }

            if (cancel)
                Height = resizeStartHeight;
            else
                ManualResized = true;

            isResizing = false;
            Capture = false;
            UninstallEscapeCancelFilter();
        }

        private void CancelResize()
        {
            EndResize(true);
            RestoreCursorIfNeeded();
        }

        private void InstallEscapeCancelFilter()
        {
            if (cancelByEscHandler != null)
            {
                return;
            }

            cancelByEscHandler = new CancelByEscHandler(this);
            Application.AddMessageFilter(cancelByEscHandler);
        }

        private void UninstallEscapeCancelFilter()
        {
            if (cancelByEscHandler == null)
            {
                return;
            }

            Application.RemoveMessageFilter(cancelByEscHandler);
            cancelByEscHandler = null;
        }

        private void v_WorkArea_Resize(object sender, EventArgs e)
        {
            AdjustControls();
            Invalidate();
        }

        private void v_WorkArea_Load(object sender, EventArgs e)
        {
            pipeControl.BackColor = UI.ClrFolderListSel;
            pipeControl.Font = UI.Font14;

            txtSource.Font = UI.FontMono12;
            txtSource.BackColor = UI.ClrChatBg;
            txtSource.ForeColor = UI.ClrFont;

            txtTransformed.Font = UI.FontMono12;
            txtTransformed.BackColor = UI.ClrChatBg;
            txtTransformed.ForeColor = UI.ClrFont;

            lblSource.BackColor = UI.ClrFolderListSel;
            lblSource.Font = UI.Font10;
            lblSource.ForeColor = UI.ClrFont;

            lblTransformed.BackColor = UI.ClrFolderListSel;
            lblTransformed.Font = UI.Font10;
            lblTransformed.ForeColor = UI.ClrFont;
        }

        private sealed class CancelByEscHandler : IMessageFilter
        {
            // ReSharper disable once InconsistentNaming
            private const int WM_KEYDOWN = 0x0100;

            private readonly v_WorkArea owner;

            public CancelByEscHandler(v_WorkArea owner)
            {
                this.owner = owner;
            }

            public bool PreFilterMessage(ref Message m)
            {
                var handled = owner.isResizing && m.Msg == WM_KEYDOWN && (Keys)(int)m.WParam == Keys.Escape;
                if (handled) owner.CancelResize();
                return handled;
            }
        }
    }
}
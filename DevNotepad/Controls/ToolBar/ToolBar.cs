using System.ComponentModel;
using Framework.AppInfrastructure;
using Framework.MVC;

namespace DevNotepad.Controls.ToolBar;

public partial class ToolBar : UserControl
{
    private const int PaddingX = 6;
    private const int PaddingY = 2;
    private const int Offset = 6;

    private static readonly Brush BrushInactiveBg = UI.BrChatListBg;
    private static readonly Brush BrushSelectedBg = UI.BrChatListSel;
    private static readonly Pen PenBorder = new(UI.ClrFolderIconSel);

    private IDataSource<VM_ToolButton> dataSource = DataSourceFactory.CreateNull<VM_ToolButton>();

    private int selectedIndex = -1;

    private Rectangle[] buttonRects = Array.Empty<Rectangle>();

    public ToolBar()
    {
        SetStyle(ControlStyles.Selectable, false);
        InitializeComponent();
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IDataSource<VM_ToolButton>? DataSource
    {
        get => dataSource;
        set
        {
            StopListeningDataSource();
            dataSource = value ?? DataSourceFactory.CreateNull<VM_ToolButton>();
            UpdateButtonRects();
            StartListeningDataSource();
            Invalidate();
        }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectedIndex
    {
        get => selectedIndex;
        set
        {
            selectedIndex = value;
            Invalidate();
            OnSelectedIndexChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public event EventHandler? OnSelectedIndexChanged;

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        if (e.Button != MouseButtons.Left) return;

        var hitIndex = GetButtonIndexAt(e.Location);
        if (hitIndex >= 0 && hitIndex != selectedIndex) SelectedIndex = hitIndex;
    }

    private int GetButtonIndexAt(Point location)
    {
        for (int i = 0; i < buttonRects.Length; i++)
            if (buttonRects[i].Contains(location))
                return i;

        return -1;
    }

    private void UpdateButtonRects()
    {
        using var g = CreateGraphics();

        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        int x = Offset;
        int y = Offset;

        var newRects = new Rectangle[dataSource.Count];

        for (int i = 0; i < dataSource.Count; i++)
        {
            var item = dataSource[i];

            var textSize = g.MeasureString(item.Text, Font, new PointF(0, 0), StringFormat.GenericTypographic);
            int textWidth = (int)Math.Ceiling(textSize.Width);
            int textHeight = (int)Math.Ceiling(textSize.Height);

            int btnWidth = textWidth + PaddingX * 2;
            int btnHeight = textHeight + PaddingY * 2;

            newRects[i] = new Rectangle(x, y, btnWidth, btnHeight);

            x += btnWidth + Offset;
        }

        buttonRects = newRects;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        var g = e.Graphics;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        for (int i = 0; i < dataSource.Count; i++)
        {
            var item = dataSource[i];
            var rect = buttonRects[i];

            bool isSelected = i == selectedIndex;
            var bgBrush = isSelected ? BrushSelectedBg : BrushInactiveBg;

            g.FillRectangle(bgBrush, rect);
            g.DrawRectangle(PenBorder, rect);

            float textX = rect.Left + PaddingX;
            float textY = rect.Top + PaddingY;
            g.DrawString(item.Text, Font, UI.BrFont, textX, textY, StringFormat.GenericTypographic);
        }
    }

    private void StartListeningDataSource()
    {
        dataSource.MessageBus.Sign<NEListChanged>(On_NEListChanged);
    }

    private void StopListeningDataSource()
    {
        dataSource.MessageBus.UnsignObject(this);
    }

    private void On_NEListChanged(NEListChanged evnt)
    {
        UpdateButtonRects();
        Invalidate();
    }

    protected override void OnFontChanged(EventArgs e)
    {
        base.OnFontChanged(e);
        UpdateButtonRects();
    }
}
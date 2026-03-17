using System.ComponentModel;
using Framework.AppInfrastructure;
using Framework.MVC;

namespace DevNotepad.Controls.ToolBar;

public partial class ToolBar : UserControl
{
    private const int PaddingX = 6;
    private const int PaddingY = 2;
    private const int Offset = 6;

    private static readonly Color ClrBack = Color.FromArgb(37, 48, 62);   // inactive bg
    private static readonly Color ClrSelected = Color.FromArgb(43, 82, 120);  // selected bg
    private static readonly Color ClrFore = Color.White;

    private static readonly SolidBrush BrushInactiveBg = new(ClrBack);
    private static readonly SolidBrush BrushSelectedBg = new(UI.ClrListSel2);
    private static readonly SolidBrush BrushFore = new(ClrFore);
    private static readonly Pen PenBorder = new(UI.ClrListSel2);

    private IDataSource<VM_ToolButton> dataSource = DataSourceFactory.CreateNull<VM_ToolButton>();
    private int selectedIndex = -1;

    public ToolBar()
    {
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
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        var g = e.Graphics;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        int x = Offset;
        int y = Offset;

        for (int i = 0; i < dataSource.Count; i++)
        {
            var item = dataSource[i];
            var textSize = g.MeasureString(item.Text, Font, new PointF(0, 0), StringFormat.GenericTypographic);
            int textWidth = (int)Math.Ceiling(textSize.Width);
            int textHeight = (int)Math.Ceiling(textSize.Height);

            int btnWidth = textWidth + PaddingX * 2;
            int btnHeight = textHeight + PaddingY * 2;

            var rect = new Rectangle(x, y, btnWidth, btnHeight);

            bool isSelected = i == selectedIndex;
            var bgBrush = isSelected ? BrushSelectedBg : BrushInactiveBg;

            g.FillRectangle(bgBrush, rect);
            g.DrawRectangle(PenBorder, rect);

            float textX = x + PaddingX;
            float textY = y + PaddingY;
            g.DrawString(item.Text, Font, BrushFore, textX, textY, StringFormat.GenericTypographic);

            x += btnWidth + Offset;
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
        Invalidate();
    }
}

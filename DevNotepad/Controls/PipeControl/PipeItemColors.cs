using System.Reflection;
using Svg;

namespace DevNotepad.Controls.PipeControl;

public class PipeItemColors
{
    public const int IconSize = 20;

    public static readonly PipeItemColors Inactive = new(UI.ClrFolderIcon, UI.ClrFont);
    public static readonly PipeItemColors Active = new(UI.ClrChatListSel, UI.ClrFont);
    public static readonly PipeItemColors Selected = new(UI.ClrFolderIconSel, Color.Black);

    public static readonly Pen PenEdit = new(UI.ClrFont);
    public static readonly Pen PenView = new(Color.Black);

    public PipeItemColors(Color bgColor, Color foreColor)
    {
        Bg = new SolidBrush(bgColor);
        Fore = new SolidBrush(foreColor);
        ParamsIcon = InitIcon("DevNotepad.Icons.gear.svg", bgColor, foreColor);
        ComposeIcon = InitIcon("DevNotepad.Icons.compose.svg", bgColor, foreColor);
    }

    public Brush Bg { get; }
    public Brush Fore { get; }
    public Image ParamsIcon { get; }
    public Image ComposeIcon { get; }

    public static Image InitIcon(string resName, Color bgColor, Color foreColor)
    {
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resName);
        if (stream == null)
            throw new Exception($"Icon '{resName}' not found in resources");

        var svgDocument = SvgDocument.Open<SvgDocument>(stream);

        SetSvgColor(svgDocument, foreColor);

        svgDocument.Width = IconSize;
        svgDocument.Height = IconSize;
        var bitmap = new Bitmap(IconSize, IconSize);
        using (var graphics = Graphics.FromImage(bitmap))
        {
            graphics.Clear(bgColor);
            svgDocument.Draw(graphics);
        }

        return bitmap;
    }

    private static void SetSvgColor(SvgElement element, Color color)
    {
        var svgColor = new SvgColourServer(color);

        if (element.Fill != SvgPaintServer.None)
            element.Fill = svgColor;

        if (element.Stroke != SvgPaintServer.None && element.Stroke != null)
            element.Stroke = svgColor;

        foreach (var child in element.Children)
        {
            SetSvgColor(child, color);
        }
    }
}
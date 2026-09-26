using DevNotepad.Core.TextTransformers;
using DevNotepad.Dialogs.TPBase;

namespace DevNotepad.Dialogs.TPJoinDlg;

public class m_TPJoinDlg : m_TPBase<JoinTransformer, p_TPJoinDlg>
{
    public string Separator
    {
        get => Transformer.Separator;
        set => eventRaiser.Raise(() => Transformer.Separator = value, p_TPJoinDlg.SeparatorChanged);
    }

    public string GroupSize
    {
        get => Transformer.GroupSize.ToString();
        set => eventRaiser.Raise(
            () =>
            {
                Transformer.GroupSize = int.TryParse(value, out var intValue) ? intValue : 0;
                if (!TemplateEnabled) Template = false;
            },
            p_TPJoinDlg.GroupSizeChanged, p_TPJoinDlg.TemplateEnabledChanged);
    }

    public bool Template
    {
        get => Transformer.Template;
        set => eventRaiser.Raise(() =>
        {
            Transformer.Template = value;

            if (!Transformer.Template) return;

            var placeholders = Enumerable
                .Range(0, Transformer.GroupSize)
                .Select(Transformer.GetLinePlaceHolder)
                .ToArray();
            if (placeholders.Any(ph => Separator.Contains(ph))) return;

            Separator = placeholders.Join(" ");
        }, p_TPJoinDlg.TemplateChanged);
    }

    public bool TemplateEnabled => Transformer.GroupSize > 1;

    public string LabelCaption => Template ? "T&emplate:" : "&Separator:";
}
using DevNotepad.Core.TextTransformers;
using DevNotepad.Dialogs.TPBase;
using static DevNotepad.Dialogs.TPSubstringDlg.p_TPSubstringDlg;

namespace DevNotepad.Dialogs.TPSubstringDlg;

[TPEditor(typeof(SubstringTransformer))]
public class c_TPSubstringDlg : c_TPBase<m_TPSubstringDlg, v_TPSubstringDlg>
{
    private readonly Dictionary<SubstringType, RadioButton> radioButtons = new();

    protected override void DoConnectModel()
    {
        base.DoConnectModel();

        radioButtons.Add(SubstringType.SkipStart, View.rbSkipStart);
        radioButtons.Add(SubstringType.SkipEnd, View.rbSkipEnd);
        radioButtons.Add(SubstringType.TakeStart, View.rbTakeStart);
        radioButtons.Add(SubstringType.TakeEnd, View.rbTakeEnd);
        radioButtons.Add(SubstringType.TakeBefore, View.rbTakeBefore);
        radioButtons.Add(SubstringType.TakeAfter, View.rbTakeAfter);

        View.Text = Model.Transformer.Caption;

        View.txtLimit.TextChanged += txtLimit_TextChanged;
        foreach (var radioButton in radioButtons.Values)
        {
            radioButton.CheckedChanged += rbType_CheckedChanged;
        }

        ApplyModelChanges(null);
    }

    protected override void DoDisconnectModel()
    {
        View.txtLimit.TextChanged -= txtLimit_TextChanged;
        foreach (var radioButton in radioButtons.Values)
        {
            radioButton.CheckedChanged -= rbType_CheckedChanged;
        }

        base.DoDisconnectModel();
    }

    protected override void OnModelChanged(int[] changeCodes)
    {
        ApplyModelChanges(changeCodes.ToEnums<p_TPSubstringDlg>());
    }

    private void ApplyModelChanges(p_TPSubstringDlg[]? changes)
    {
        changes ??= Enums.Values<p_TPSubstringDlg>();

        modelSuppressor.Exec(() =>
        {
            if (changes.Contains(LimitChanged))
                View.txtLimit.Text = Model.Limit;

            if (changes.Contains(TypeChanged))
            {
                foreach (var (type, radioButton) in radioButtons)
                {
                    if (Model.Type == type)
                    {
                        radioButton.Checked = true;
                        break;
                    }
                }
            }

            UI.UnfocusCheckBox(View.txtLimit, changes, TypeChanged);
        });
    }

    private void txtLimit_TextChanged(object? sender, EventArgs e)
    {
        if (modelSuppressor.Suppress) return;
        Model.Limit = View.txtLimit.Text;
    }

    private void rbType_CheckedChanged(object? sender, EventArgs e)
    {
        if (modelSuppressor.Suppress) return;

        foreach (var (type, radioButton) in radioButtons)
        {
            if (radioButton.Checked)
            {
                Model.Type = type;
                break;
            }
        }
    }
}
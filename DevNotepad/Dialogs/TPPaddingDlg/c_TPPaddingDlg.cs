using DevNotepad.Core.TextTransformers;
using DevNotepad.Dialogs.TPBase;
using static DevNotepad.Dialogs.TPPaddingDlg.p_TPPaddingDlg;

namespace DevNotepad.Dialogs.TPPaddingDlg;

[TPEditor(typeof(PaddingTransformer))]
public class c_TPPaddingDlg : c_TPBase<m_TPPaddingDlg, v_TPPaddingDlg>
{
    private readonly Dictionary<PaddingType, RadioButton> radioButtons = new();

    protected override void DoConnectModel()
    {
        base.DoConnectModel();

        radioButtons.Add(PaddingType.Leading, View.rbLeading);
        radioButtons.Add(PaddingType.Trailing, View.rbTrailing);

        View.Text = Model.Transformer.Caption;

        View.txtTotalLength.TextChanged += txtTotalLength_TextChanged;
        View.txtSymbol.TextChanged += txtSymbol_TextChanged;
        foreach (var radioButton in radioButtons.Values)
        {
            radioButton.CheckedChanged += rbPaddingType_CheckedChanged;
        }

        ApplyModelChanges(null);
    }

    protected override void DoDisconnectModel()
    {
        View.txtTotalLength.TextChanged -= txtTotalLength_TextChanged;
        View.txtSymbol.TextChanged -= txtSymbol_TextChanged;
        foreach (var radioButton in radioButtons.Values)
        {
            radioButton.CheckedChanged -= rbPaddingType_CheckedChanged;
        }

        base.DoDisconnectModel();
    }

    protected override void OnModelChanged(int[] changeCodes)
    {
        ApplyModelChanges(changeCodes.ToEnums<p_TPPaddingDlg>());
    }

    private void ApplyModelChanges(p_TPPaddingDlg[]? changes)
    {
        changes ??= Enums.Values<p_TPPaddingDlg>();

        modelSuppressor.Exec(() =>
        {
            if (changes.Contains(TotalLengthChanged))
                View.txtTotalLength.Text = Model.TotalLength;

            if (changes.Contains(SymbolChanged))
                View.txtSymbol.Text = Model.Symbol;

            if (changes.Contains(PaddingTypeChanged))
            {
                foreach (var (type, radioButton) in radioButtons)
                {
                    if (Model.PaddingType == type)
                    {
                        radioButton.Checked = true;
                        break;
                    }
                }
            }

            UI.UnfocusCheckBox(View.txtTotalLength, changes, PaddingTypeChanged);
        });
    }

    private void txtTotalLength_TextChanged(object? sender, EventArgs e)
    {
        if (modelSuppressor.Suppress) return;
        Model.TotalLength = View.txtTotalLength.Text;
    }

    private void txtSymbol_TextChanged(object? sender, EventArgs e)
    {
        if (modelSuppressor.Suppress) return;
        Model.Symbol = View.txtSymbol.Text;
    }

    private void rbPaddingType_CheckedChanged(object? sender, EventArgs e)
    {
        if (modelSuppressor.Suppress) return;

        foreach (var (type, radioButton) in radioButtons)
        {
            if (radioButton.Checked)
            {
                Model.PaddingType = type;
                break;
            }
        }
    }
}

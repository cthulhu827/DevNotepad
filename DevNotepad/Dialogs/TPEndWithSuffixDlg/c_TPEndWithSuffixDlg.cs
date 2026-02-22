using DevNotepad.Core;
using DevNotepad.Core.TextTransformers;
using DevNotepad.Dialogs.TPBase;

namespace DevNotepad.Dialogs.TPEndWithSuffixDlg;

[TPEditor(typeof(EndWithSuffixTransformer))]
public class c_TPEndWithSuffixDlg : c_TPBase<m_TPEndWithSuffixDlg, v_TPEndWithSuffixDlg>
{
    protected override void DoConnectModel()
    {
        base.DoConnectModel();

        View.Text = Model.Transformer.Caption;

        View.txtSuffix.TextChanged += txtSuffix_TextChanged;
        View.chkExceptLastLine.CheckedChanged += chkExceptLastLine_CheckedChanged;

        ApplyModelChanges(null);
    }

    private void txtSuffix_TextChanged(object? sender, EventArgs e)
    {
        if (modelSuppressor.Suppress) return;
        Model.Suffix = View.txtSuffix.Text;
    }

    private void chkExceptLastLine_CheckedChanged(object? sender, EventArgs e)
    {
        if (modelSuppressor.Suppress) return;
        Model.ExceptLastLine = View.chkExceptLastLine.Checked;
    }

    protected override void DoDisconnectModel()
    {
        View.txtSuffix.TextChanged -= txtSuffix_TextChanged;
        View.chkExceptLastLine.CheckedChanged -= chkExceptLastLine_CheckedChanged;

        base.DoDisconnectModel();
    }

    protected override void OnModelChanged(int[] changeCodes)
    {
        ApplyModelChanges(changeCodes.ToEnums<p_TPEndWithSuffixDlg>());
    }

    private void ApplyModelChanges(p_TPEndWithSuffixDlg[]? changes)
    {
        changes ??= Enums.Values<p_TPEndWithSuffixDlg>();

        modelSuppressor.Exec(() =>
        {
            if (changes.Contains(p_TPEndWithSuffixDlg.SuffixChanged))
                View.txtSuffix.Text = Model.Suffix;
            if (changes.Contains(p_TPEndWithSuffixDlg.ExceptLastLineChanged))
                View.chkExceptLastLine.Checked = Model.ExceptLastLine;

            UI.UnfocusCheckBox(View.txtSuffix, changes, p_TPEndWithSuffixDlg.ExceptLastLineChanged);
        });
    }
}
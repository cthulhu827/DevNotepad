using DevNotepad.Core;
using DevNotepad.Core.TextTransformers;
using DevNotepad.Dialogs.TPBase;

namespace DevNotepad.Dialogs.TPTrimTextDlg;

[TPEditor(typeof(TrimTextTransformer))]
public class c_TPTrimTextDlg : c_TPBase<m_TPTrimTextDlg, v_TPTrimTextDlg>
{
    protected override void DoConnectModel()
    {
        base.DoConnectModel();

        View.Text = Model.Transformer.Caption;

        View.chkAutoPrefix.CheckedChanged += chkAutoPrefix_CheckedChanged;
        View.txtPrefix.TextChanged += txtPrefix_TextChanged;
        View.chkAutoSuffix.CheckedChanged += chkAutoSuffix_CheckedChanged;
        View.txtSuffix.TextChanged += txtSuffix_TextChanged;

        ApplyModelChanges(null);
    }

    protected override void DoDisconnectModel()
    {
        View.chkAutoPrefix.CheckedChanged -= chkAutoPrefix_CheckedChanged;
        View.txtPrefix.TextChanged -= txtPrefix_TextChanged;
        View.chkAutoSuffix.CheckedChanged -= chkAutoSuffix_CheckedChanged;
        View.txtSuffix.TextChanged -= txtSuffix_TextChanged;

        base.DoDisconnectModel();
    }

    protected override void OnModelChanged(int[] changeCodes)
    {
        ApplyModelChanges(changeCodes.ToEnums<p_TPTrimTextDlg>());
    }

    private void ApplyModelChanges(p_TPTrimTextDlg[]? changes)
    {
        changes ??= Enums.Values<p_TPTrimTextDlg>();

        modelSuppressor.Exec(() =>
        {
            if (changes.Contains(p_TPTrimTextDlg.AutoPrefixChanged))
            {
                View.chkAutoPrefix.Checked = Model.AutoPrefix;
                View.txtPrefix.ReadOnly = Model.AutoPrefix;
                View.txtPrefix.ForeColor = Model.AutoPrefix ? UI.ClrEditForeDisabled : UI.ClrListFore;
                View.txtPrefix.BackColor = Model.AutoPrefix ? UI.ClrBack : UI.ClrEditBack;

                if (!Model.AutoPrefix) View.txtPrefix.Focus();
            }

            if (changes.Contains(p_TPTrimTextDlg.PrefixChanged))
                View.txtPrefix.Text = Model.Prefix;

            if (changes.Contains(p_TPTrimTextDlg.AutoSuffixChanged))
            {
                View.chkAutoSuffix.Checked = Model.AutoSuffix;
                View.txtSuffix.ReadOnly = Model.AutoSuffix;
                View.txtSuffix.ForeColor = Model.AutoSuffix ? UI.ClrEditForeDisabled : UI.ClrListFore;
                View.txtSuffix.BackColor = Model.AutoSuffix ? UI.ClrBack : UI.ClrEditBack;

                if (!Model.AutoSuffix && !changes.Contains(p_TPTrimTextDlg.AutoPrefixChanged)) 
                    View.txtSuffix.Focus();
            }

            if (changes.Contains(p_TPTrimTextDlg.SuffixChanged))
                View.txtSuffix.Text = Model.Suffix;
        });
    }

    private void chkAutoPrefix_CheckedChanged(object? sender, EventArgs e)
    {
        if (modelSuppressor.Suppress) return;
        Model.AutoPrefix = View.chkAutoPrefix.Checked;
    }

    private void txtPrefix_TextChanged(object? sender, EventArgs e)
    {
        if (modelSuppressor.Suppress) return;
        Model.Prefix = View.txtPrefix.Text;
    }

    private void chkAutoSuffix_CheckedChanged(object? sender, EventArgs e)
    {
        if (modelSuppressor.Suppress) return;
        Model.AutoSuffix = View.chkAutoSuffix.Checked;
    }

    private void txtSuffix_TextChanged(object? sender, EventArgs e)
    {
        if (modelSuppressor.Suppress) return;
        Model.Suffix = View.txtSuffix.Text;
    }
}
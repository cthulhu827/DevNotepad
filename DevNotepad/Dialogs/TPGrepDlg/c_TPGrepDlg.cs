using DevNotepad.Core.TextTransformers;
using DevNotepad.Dialogs.TPBase;
using static DevNotepad.Dialogs.TPGrepDlg.p_TPGrepDlg;

namespace DevNotepad.Dialogs.TPGrepDlg;

[TPEditor(typeof(GrepTransformer))]
public class c_TPGrepDlg : c_TPBase<m_TPGrepDlg, v_TPGrepDlg>
{
    protected override void DoConnectModel()
    {
        base.DoConnectModel();

        View.Text = Model.Transformer.Caption;

        View.txtSearchText.TextChanged += txtSearchText_TextChanged;
        View.chkExclude.CheckedChanged += chkExclude_CheckedChanged;
        View.chkCaseSensitive.CheckedChanged += chkCaseSensitive_CheckedChanged;
        View.chkRegEx.CheckedChanged += chkRegEx_CheckedChanged;
        View.txtLinesBefore.TextChanged += txtLinesBefore_TextChanged;
        View.txtLinesAfter.TextChanged += txtLinesAfter_TextChanged;
        View.chkDoNotSeparate.CheckedChanged += chkDoNotSeparate_CheckedChanged;

        ApplyModelChanges(null);
    }

    protected override void DoDisconnectModel()
    {
        View.txtSearchText.TextChanged -= txtSearchText_TextChanged;
        View.chkExclude.CheckedChanged -= chkExclude_CheckedChanged;
        View.chkCaseSensitive.CheckedChanged -= chkCaseSensitive_CheckedChanged;
        View.chkRegEx.CheckedChanged -= chkRegEx_CheckedChanged;
        View.txtLinesBefore.TextChanged -= txtLinesBefore_TextChanged;
        View.txtLinesAfter.TextChanged -= txtLinesAfter_TextChanged;
        View.chkDoNotSeparate.CheckedChanged -= chkDoNotSeparate_CheckedChanged;

        base.DoDisconnectModel();
    }

    protected override void OnModelChanged(int[] changeCodes)
    {
        ApplyModelChanges(changeCodes.ToEnums<p_TPGrepDlg>());
    }

    private void ApplyModelChanges(p_TPGrepDlg[]? changes)
    {
        changes ??= Enums.Values<p_TPGrepDlg>();

        modelSuppressor.Exec(() =>
        {
            if (changes.Contains(SearchTextChanged))
                View.txtSearchText.Text = Model.SearchText;

            if (changes.Contains(ExcludeChanged))
                View.chkExclude.Checked = Model.Exclude;

            if (changes.Contains(CaseSensitiveChanged))
                View.chkCaseSensitive.Checked = Model.CaseSensitive;

            if (changes.Contains(RegExChanged))
            {
                View.chkRegEx.Checked = Model.RegEx;
                View.chkCaseSensitive.Enabled = !Model.RegEx;
            }

            if (changes.Contains(LinesBeforeChanged))
            {
                View.txtLinesBefore.Text = Model.LinesBefore;
                View.chkDoNotSeparate.Enabled = Model.LinesAfter != "0" || Model.LinesBefore != "0";
            }

            if (changes.Contains(LinesAfterChanged))
            {
                View.txtLinesAfter.Text = Model.LinesAfter;
                View.chkDoNotSeparate.Enabled = Model.LinesAfter != "0" || Model.LinesBefore != "0";
            }

            if (changes.Contains(DoNotSeparateChanged))
            {
                View.chkDoNotSeparate.Checked = Model.DoNotSeparate;
            }

            UI.UnfocusCheckBox(View.txtSearchText, changes, ExcludeChanged, CaseSensitiveChanged, RegExChanged);
        });
    }

    private void txtSearchText_TextChanged(object? sender, EventArgs e)
    {
        if (modelSuppressor.Suppress) return;
        Model.SearchText = View.txtSearchText.Text;
    }

    private void chkExclude_CheckedChanged(object? sender, EventArgs e)
    {
        if (modelSuppressor.Suppress) return;
        Model.Exclude = View.chkExclude.Checked;
    }

    private void chkCaseSensitive_CheckedChanged(object? sender, EventArgs e)
    {
        if (modelSuppressor.Suppress) return;
        Model.CaseSensitive = View.chkCaseSensitive.Checked;
    }

    private void chkRegEx_CheckedChanged(object? sender, EventArgs e)
    {
        if (modelSuppressor.Suppress) return;
        Model.RegEx = View.chkRegEx.Checked;
    }

    private void txtLinesBefore_TextChanged(object? sender, EventArgs e)
    {
        if (modelSuppressor.Suppress) return;
        Model.LinesBefore = View.txtLinesBefore.Text;
    }

    private void txtLinesAfter_TextChanged(object? sender, EventArgs e)
    {
        if (modelSuppressor.Suppress) return;
        Model.LinesAfter = View.txtLinesAfter.Text;
    }

    private void chkDoNotSeparate_CheckedChanged(object? sender, EventArgs e)
    {
        if (modelSuppressor.Suppress) return;
        Model.DoNotSeparate = View.chkDoNotSeparate.Checked;
    }
}
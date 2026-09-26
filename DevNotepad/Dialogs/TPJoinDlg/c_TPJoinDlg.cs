using DevNotepad.Core.TextTransformers;
using DevNotepad.Dialogs.TPBase;
using static DevNotepad.Dialogs.TPJoinDlg.p_TPJoinDlg;

namespace DevNotepad.Dialogs.TPJoinDlg;

[TPEditor(typeof(JoinTransformer))]
public class c_TPJoinDlg : c_TPBase<m_TPJoinDlg, v_TPJoinDlg>
{
    protected override void DoConnectModel()
    {
        base.DoConnectModel();

        View.Text = Model.Transformer.Caption;

        View.txtSeparator.TextChanged += txtSeparator_TextChanged;
        View.txtGroupSize.TextChanged += txtGroupSize_TextChanged;
        View.chkTemplate.CheckedChanged += chkTemplate_CheckedChanged;

        ApplyModelChanges(null);
    }

    protected override void DoDisconnectModel()
    {
        View.txtSeparator.TextChanged -= txtSeparator_TextChanged;
        View.txtGroupSize.TextChanged -= txtGroupSize_TextChanged;
        View.chkTemplate.CheckedChanged -= chkTemplate_CheckedChanged;

        base.DoDisconnectModel();
    }

    protected override void OnModelChanged(int[] changeCodes)
    {
        ApplyModelChanges(changeCodes.ToEnums<p_TPJoinDlg>());
    }

    private void ApplyModelChanges(p_TPJoinDlg[]? changes)
    {
        changes ??= Enums.Values<p_TPJoinDlg>();

        modelSuppressor.Exec(() =>
        {
            if (changes.Contains(SeparatorChanged))
                View.txtSeparator.Text = Model.Separator;

            if (changes.Contains(GroupSizeChanged))
                View.txtGroupSize.Text = Model.GroupSize;

            if (changes.Contains(TemplateChanged))
            {
                View.chkTemplate.Checked = Model.Template;
                View.lblSeparator.Text = Model.LabelCaption;
            }

            if (changes.Contains(TemplateEnabledChanged))
                View.chkTemplate.Enabled = Model.TemplateEnabled;

            UI.UnfocusCheckBox(View.txtSeparator, changes, TemplateChanged);
        });
    }

    private void txtSeparator_TextChanged(object? sender, EventArgs e)
    {
        if (modelSuppressor.Suppress) return;
        Model.Separator = View.txtSeparator.Text;
    }

    private void txtGroupSize_TextChanged(object? sender, EventArgs e)
    {
        if (modelSuppressor.Suppress) return;
        Model.GroupSize = View.txtGroupSize.Text;
    }

    private void chkTemplate_CheckedChanged(object? sender, EventArgs e)
    {
        if (modelSuppressor.Suppress) return;
        Model.Template = View.chkTemplate.Checked;
    }
}
using DevNotepad.Core.TextTransformers;
using DevNotepad.Dialogs.TPBase;
using static DevNotepad.Dialogs.TPReplaceDlg.p_TPReplaceDlg;

namespace DevNotepad.Dialogs.TPReplaceDlg;

[TPEditor(typeof(ReplaceTransformer))]
public class c_TPReplaceDlg : c_TPBase<m_TPReplaceDlg, v_TPReplaceDlg>
{
    protected override void DoConnectModel()
    {
        base.DoConnectModel();

        View.Text = Model.Transformer.Caption;

        View.txtOldValue.TextChanged += txtOldValue_TextChanged;
        View.txtNewValue.TextChanged += txtNewValue_TextChanged;

        ApplyModelChanges(null);
    }

    protected override void DoDisconnectModel()
    {
        View.txtOldValue.TextChanged -= txtOldValue_TextChanged;
        View.txtNewValue.TextChanged -= txtNewValue_TextChanged;

        base.DoDisconnectModel();
    }

    protected override void OnModelChanged(int[] changeCodes)
    {
        ApplyModelChanges(changeCodes.ToEnums<p_TPReplaceDlg>());
    }

    private void ApplyModelChanges(p_TPReplaceDlg[]? changes)
    {
        changes ??= Enums.Values<p_TPReplaceDlg>();

        modelSuppressor.Exec(() =>
        {
            if (changes.Contains(OldValueChanged))
                View.txtOldValue.Text = Model.OldValue;

            if (changes.Contains(NewValueChanged))
                View.txtNewValue.Text = Model.NewValue;
        });
    }

    private void txtOldValue_TextChanged(object? sender, EventArgs e)
    {
        if (modelSuppressor.Suppress) return;
        Model.OldValue = View.txtOldValue.Text;
    }

    private void txtNewValue_TextChanged(object? sender, EventArgs e)
    {
        if (modelSuppressor.Suppress) return;
        Model.NewValue = View.txtNewValue.Text;
    }
}
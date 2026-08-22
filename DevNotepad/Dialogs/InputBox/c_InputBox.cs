using DevNotepad.Dialogs.TPSingleParamDlg;
using Framework.MVC;

namespace DevNotepad.Dialogs.InputBox;

public class c_InputBox : ModalDialogController<m_InputBox, v_TPSingleParamDlg>
{
    protected override void DoConnectModel()
    {
        base.DoConnectModel();

        View.Text = Model.Caption;
        View.lblHint.Text = "";

        View.txtParameterText.TextChanged += txtParameter_TextChanged;

        ApplyModelChanges(null);
    }

    protected override void DoDisconnectModel()
    {
        View.txtParameterText.TextChanged -= txtParameter_TextChanged;

        base.DoDisconnectModel();
    }

    protected override void OnModelChanged(int[] changeCodes)
    {
        ApplyModelChanges(changeCodes.ToEnums<p_InputBox>());
    }

    private void ApplyModelChanges(p_InputBox[]? changes)
    {
        changes ??= Enums.Values<p_InputBox>();

        modelSuppressor.Exec(() =>
        {
            if (changes.Contains(p_InputBox.TextChanged))
                View.txtParameterText.Text = Model.Text;
        });
    }

    private void txtParameter_TextChanged(object? sender, EventArgs e)
    {
        if (modelSuppressor.Suppress) return;
        Model.Text = View.txtParameterText.Text;
    }
}
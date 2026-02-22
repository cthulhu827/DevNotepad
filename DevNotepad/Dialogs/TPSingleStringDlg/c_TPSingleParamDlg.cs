using DevNotepad.Core;
using DevNotepad.Core.TextTransformers;
using DevNotepad.Dialogs.TPBase;
using Framework.MVC;

namespace DevNotepad.Dialogs.TPSingleParamDlg;

[TPEditor(typeof(TemplateTransformer))]
public class c_TPTemplateTransformerDlg : c_TPSingleParamDlg<m_TPSingleParamDlg<TemplateTransformer>>
{
}

[TPEditor(typeof(FileSizeTransformer))]
public class c_TPFileSizeTransformerDlg : c_TPSingleParamDlg<m_TPSingleParamDlg<FileSizeTransformer>>
{
}

[TPEditor(typeof(SplitTransformer))]
public class c_TPSplitTransformerDlg : c_TPSingleParamDlg<m_TPSingleParamDlg<SplitTransformer>>
{
}

[TPEditor(typeof(JoinTransformer))]
public class c_TPJoinTransformerDlg : c_TPSingleParamDlg<m_TPSingleParamDlg<JoinTransformer>>
{
}

public class c_TPSingleParamDlg<TModel> : c_TPBase<TModel, v_TPSingleParamDlg>
    where TModel : MVC_Model, ITPDlgModel, ISingleParameterTextTransformer, new()
{
    protected override void DoConnectModel()
    {
        base.DoConnectModel();

        View.Text = Model.Transformer.Caption;

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
        ApplyModelChanges(changeCodes.ToEnums<p_TPSingleParamDlg>());
    }

    private void ApplyModelChanges(p_TPSingleParamDlg[]? changes)
    {
        changes ??= Enums.Values<p_TPSingleParamDlg>();

        modelSuppressor.Exec(() =>
        {
            if (changes.Contains(p_TPSingleParamDlg.ParameterChanged))
                View.txtParameterText.Text = Model.Parameter;
        });
    }

    private void txtParameter_TextChanged(object? sender, EventArgs e)
    {
        if (modelSuppressor.Suppress) return;
        Model.Parameter = View.txtParameterText.Text;
    }
}
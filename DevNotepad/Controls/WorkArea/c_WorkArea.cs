using DevNotepad.Controls.PipeControl;
using DevNotepad.Core;
using DevNotepad.Infrastructure;
using Framework.MVC;

namespace DevNotepad.Controls.WorkArea;

public class c_WorkArea : MVC_Controller<m_WorkArea, v_WorkArea>, IKeyHandler
{
    protected override void DoConnectModel()
    {
        base.DoConnectModel();

        View.txtSource.TextChanged += TxtSource_TextChanged;
        View.pipeControl.OnSelectedIndexChanged += PipeControl_OnSelectedIndexChanged;
        View.pipeControl.OnInsertItem += PipeControl_OnInsertItem;
        View.pipeControl.OnEditItem += PipeControl_OnEditItem;
        View.pipeControl.OnDeleteItem += PipeControl_OnDeleteItem;

        View.pipeControl.DataSource = Model.Pipe;
        ApplyModelChanges(null);
    }

    protected override void DoDisconnectModel()
    {
        View.pipeControl.DataSource = null;

        View.txtSource.TextChanged -= TxtSource_TextChanged;
        View.pipeControl.OnSelectedIndexChanged -= PipeControl_OnSelectedIndexChanged;
        View.pipeControl.OnInsertItem -= PipeControl_OnInsertItem;
        View.pipeControl.OnEditItem -= PipeControl_OnEditItem;
        View.pipeControl.OnDeleteItem -= PipeControl_OnDeleteItem;

        base.DoDisconnectModel();
    }

    protected override void OnModelChanged(int[] changeCodes)
    {
        ApplyModelChanges(changeCodes.ToEnums<p_WorkArea>());
    }

    private void ApplyModelChanges(p_WorkArea[]? changes)
    {
        changes ??= Enums.Values<p_WorkArea>();

        modelSuppressor.Exec(() =>
        {
            if (changes.Contains(p_WorkArea.SourceChanged))
            {
                View.txtSource.Lines = Model.Source;
                View.lblSource.Text = Model.SourceDescription;
            }

            if (changes.Contains(p_WorkArea.TransformedChanged))
            {
                View.txtTransformed.Lines = Model.Transformed;
                View.lblTransformed.Text = Model.TransformedDescription;
            }

            if (changes.Contains(p_WorkArea.PipeIndexChanged))
            {
                View.pipeControl.SelectedIndex = Model.PipeIndex;
            }
        });
    }

    private void TxtSource_TextChanged(object? sender, EventArgs e)
    {
        if (modelSuppressor.Suppress) return;
        Model.Source = View.txtSource.Lines;
    }

    private void PipeControl_OnSelectedIndexChanged(object? sender, EventArgs e)
    {
        if (modelSuppressor.Suppress) return;
        Model.PipeIndex = View.pipeControl.SelectedIndex;
    }

    private void PipeControl_OnInsertItem(object? sender, PipeInsertItemEventArgs e)
    {
        var transformerId = UI.AskTransformer();
        if (transformerId == null) return;

        var transformer = Domain.CreateById(transformerId.Value);
        Model.InsertItem(transformer, e.Before);
    }

    private void PipeControl_OnEditItem(object? sender, PipeEditItemEventArgs e)
    {
        Model.EditSelectedItem(e.Shift);
    }

    private void PipeControl_OnDeleteItem(object? sender, EventArgs e)
    {
        Model.DeleteSelectedItem();
    }

    private void AddTransformer()
    {
        var transformerId = UI.AskTransformer();
        if (transformerId == null) return;

        var transformer = Domain.CreateById(transformerId.Value);
        Model.AddItem(transformer);
    }

    #region IKeyHandler implementation

    public bool HandleKey(KeyEventArgs e)
    {
        if (!View.ContainsFocus) return false;

        var result = true;
        if (e.KeyCode == Keys.F1)
            View.txtSource.Text = Clipboard.GetText().TrimEnd();
        else if (e.KeyCode == Keys.F2)
            Clipboard.SetText(View.txtTransformed.Text);
        else if (e.KeyCode == Keys.F4)
            View.pipeControl.Edit(View.ActiveControl);
        if (e.KeyCode == Keys.Insert)
            AddTransformer();
        else
            result = false;

        return result;
    }

    #endregion

    public void SetFocus()
    {
        View.txtSource.Focus();
    }
}
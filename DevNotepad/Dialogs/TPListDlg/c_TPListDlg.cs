using DevNotepad.Controls.StringLists;
using DevNotepad.Core.TextTransformers;
using DevNotepad.Dialogs.TPBase;
using static DevNotepad.Dialogs.TPListDlg.p_TPListDlg;

namespace DevNotepad.Dialogs.TPListDlg;

[TPEditor(typeof(ListTransformer))]
public class c_TPListDlg : c_TPBase<m_TPListDlg, v_TPListDlg>
{
    private readonly Dictionary<ListOperation, RadioButton> radioButtons = new();

    protected override void DoConnectModel()
    {
        base.DoConnectModel();

        View.lbList1.DataSource = Model.DsList1;
        View.lbList2.DataSource = Model.DsList2;

        radioButtons.Add(ListOperation.Exclude, View.rbExclude);
        radioButtons.Add(ListOperation.Intersect, View.rbIntersect);
        radioButtons.Add(ListOperation.Union, View.rbUnion);
        radioButtons.Add(ListOperation.Concat, View.rbConcat);

        View.Text = Model.Transformer.Caption;

        View.lbList1.SelectedIndexChanged += lbList1_SelectedIndexChanged;
        View.lbList2.SelectedIndexChanged += lbList2_SelectedIndexChanged;

        foreach (var radioButton in radioButtons.Values)
        {
            radioButton.CheckedChanged += rbOperation_CheckedChanged;
        }

        ApplyModelChanges(null);
    }

    protected override void DoDisconnectModel()
    {
        foreach (var radioButton in radioButtons.Values)
        {
            radioButton.CheckedChanged -= rbOperation_CheckedChanged;
        }

        View.lbList1.SelectedIndexChanged -= lbList1_SelectedIndexChanged;
        View.lbList2.SelectedIndexChanged -= lbList2_SelectedIndexChanged;

        View.lbList1.DataSource = null;
        View.lbList2.DataSource = null;

        base.DoDisconnectModel();
    }

    protected override void OnModelChanged(int[] changeCodes)
    {
        ApplyModelChanges(changeCodes.ToEnums<p_TPListDlg>());
    }

    private void ApplyModelChanges(p_TPListDlg[]? changes)
    {
        changes ??= Enums.Values<p_TPListDlg>();

        modelSuppressor.Exec(() =>
        {
            if (changes.Contains(List1Changed))
            {
                View.lbList1.SelectedItem = Model.DsList1.Items
                    .FirstOrDefault(vm => vm.List == Model.List1);
            }

            if (changes.Contains(List2Changed))
            {
                View.lbList2.SelectedItem = Model.DsList2.Items
                    .FirstOrDefault(vm => vm.List == Model.List2);
            }

            if (changes.Contains(OperationChanged))
            {
                foreach (var (operation, radioButton) in radioButtons)
                {
                    if (Model.Operation == operation)
                    {
                        radioButton.Checked = true;
                        break;
                    }
                }
            }
        });
    }

    private void rbOperation_CheckedChanged(object? sender, EventArgs e)
    {
        if (modelSuppressor.Suppress) return;

        foreach (var (operation, radioButton) in radioButtons)
        {
            if (radioButton.Checked)
            {
                Model.Operation = operation;
                break;
            }
        }
    }

    private void lbList1_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (modelSuppressor.Suppress) return;

        Model.List1 = (View.lbList1.SelectedItem as VM_StringList)?.List;
    }

    private void lbList2_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (modelSuppressor.Suppress) return;

        Model.List2 = (View.lbList2.SelectedItem as VM_StringList)?.List;
    }
}
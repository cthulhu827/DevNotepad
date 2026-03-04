using DevNotepad.Controls.Page;
using DevNotepad.Controls.PipeControl;
using DevNotepad.Controls.WorkArea;
using DevNotepad.Core;
using DevNotepad.Core.TextTransformers;
using Framework.MVC;

namespace DevNotepad.Controls.MainWindow;

public class c_MainWindow : MVC_Controller<m_MainWindow, v_MainWindow>
{
    private readonly c_Page cPage = new c_Page();

    protected override void DoConnectView()
    {
        base.DoConnectView();

        cPage.View = View.v_Page1;
    }

    protected override void DoDisconnectView()
    {
        cPage.ViewNullable = null;

        base.DoDisconnectView();
    }

    protected override void DoConnectModel()
    {
        base.DoConnectModel();

        cPage.Model = new m_Page();
        cPage.Model.AddWorkArea();

        View.KeyDown += View_KeyDown;
        View.button1.Click += Button1_Click;
    }

    protected override void DoDisconnectModel()
    {
        View.KeyDown -= View_KeyDown;
        View.button1.Click -= Button1_Click;

        cPage.ModelNullable = null;

        base.DoDisconnectModel();
    }

    private m_WorkArea InitWithTestData(m_WorkArea? mWorkArea)
    {
        var lines = new[] { "Line1", "Line3", "Line1", "Line2" };
        mWorkArea ??= new m_WorkArea();
        mWorkArea.Source = lines;
        return mWorkArea;
        mWorkArea.Pipe.BeginUpdate();
        try
        {
            mWorkArea.Pipe.AddItem(new VM_PipeItem(new DistinctTransformer()));
            mWorkArea.Pipe.AddItem(new VM_PipeItem(new SortTransformer()));
            mWorkArea.Pipe.AddItem(new VM_PipeItem(new QuotesTextTransformer('"')));
        }
        finally
        {
            mWorkArea.Pipe.EndUpdate();
        }

        return mWorkArea;
    }

    private void Button1_Click(object? sender, EventArgs e)
    {
    }

    private void View_KeyDown(object? sender, KeyEventArgs e)
    {
        var active = GetActiveWorkArea();
        var vWorkArea = active?.View;
        if (e.KeyCode == Keys.F1)
        {
            if (vWorkArea != null) vWorkArea.txtSource.Text = Clipboard.GetText();
        }
        else if (e.KeyCode == Keys.F2)
        {
            if (vWorkArea != null) Clipboard.SetText(vWorkArea.txtTransformed.Text);
        }
        else if (e.KeyCode == Keys.Insert)
        {
            AddTransformer();
        }
        else if (e.KeyCode == Keys.F4)
        {
            vWorkArea?.pipeControl.Edit(vWorkArea.ActiveControl);
        }
        else if (e.KeyCode == Keys.F5)
        {
            if (active != null) active.Model = InitWithTestData(null);
        }
        else if (e is { KeyCode: Keys.N, Control: true })
        {
            var model = CopyModel(active?.Model, e.Shift, e.Alt);
            cPage.Model.AddWorkArea(model);
        }
    }

    private static m_WorkArea? CopyModel(m_WorkArea? modelToCopy, bool copyPipe, bool copySource)
    {
        if (modelToCopy == null || (!copyPipe && !copySource)) return null;

        var result = new m_WorkArea();

        if (copyPipe)
            foreach (var vm in modelToCopy.Pipe.Items)
            {
                result.Pipe.AddItem(new VM_PipeItem(vm.Transformer.Copy()));
            }

        if (copySource)
        {
            result.Source = new string[modelToCopy.Source.Length];
            modelToCopy.Source.CopyTo(result.Source, 0);
        }

        return result;
    }

    private void AddTransformer()
    {
        var active = GetActiveWorkArea();
        if (active == null) return;

        var transformerId = UI.AskTransformer();
        if (transformerId == null) return;

        var transformer = Domain.CreateById(transformerId.Value);
        active.Model.AddItem(transformer);
    }

    private c_WorkArea? GetActiveWorkArea()
    {
        var vWorkArea = FindFocusedWorkArea();
        return vWorkArea == null ? null : cPage.workAreas.FirstOrDefault(c => c.View == vWorkArea);
    }

    public v_WorkArea? FindFocusedWorkArea()
    {
        Control control = View;
        var container = control as IContainerControl;
        while (container != null)
        {
            control = container.ActiveControl;
            if (control is v_WorkArea result) return result;
            container = control as IContainerControl;
        }

        return null;
    }
}
using DevNotepad.Controls.Page;
using DevNotepad.Infrastructure;
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

    private void Button1_Click(object? sender, EventArgs e)
    {
    }

    private void View_KeyDown(object? sender, KeyEventArgs e)
    {
        HandleKey(cPage, e);
    }

    private bool HandleKey(IKeyHandler keyHandler, KeyEventArgs e)
    {
        if (keyHandler.HandleKey(e)) return true;
        foreach (var nested in keyHandler.NestedKeyHandlers)
        {
            if (HandleKey(nested, e)) return true;
        }

        return false;
    }
}
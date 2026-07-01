using DevNotepad.Controls.Page;
using DevNotepad.Infrastructure;
using Framework.MVC;

namespace DevNotepad.Controls.MainWindow;

public class c_MainWindow : MVC_Controller<m_MainWindow, v_MainWindow>, IKeyHandler
{
    private c_Page[] pages = Array.Empty<c_Page>();

    protected override void DoConnectModel()
    {
        base.DoConnectModel();

        View.KeyDown += View_KeyDown;
        View.tbPages.OnSelectedIndexChanged += TbPages_OnSelectedIndexChanged;
        View.tmrTimer.Tick += TmrTimer_Tick;
        View.button1.Click += Button1_Click;

        View.tbPages.DataSource = Model.ToolButtons;

        ApplyModelChanges(null);
    }

    protected override void DoDisconnectModel()
    {
        View.tbPages.DataSource = null;

        View.KeyDown -= View_KeyDown;
        View.tbPages.OnSelectedIndexChanged -= TbPages_OnSelectedIndexChanged;
        View.tmrTimer.Tick -= TmrTimer_Tick;
        View.button1.Click -= Button1_Click;

        base.DoDisconnectModel();
    }

    protected override void OnModelChanged(int[] changeCodes)
    {
        ApplyModelChanges(changeCodes.ToEnums<p_MainWindow>());
    }

    private void ApplyModelChanges(p_MainWindow[]? changes)
    {
        changes ??= Enums.Values<p_MainWindow>();

        modelSuppressor.Exec(() =>
        {
            if (changes.Contains(p_MainWindow.PageAdded))
            {
                UpdatePagesByModel();
                View.tbPages.Visible = Model.Pages.Length > 1;
            }

            if (changes.Contains(p_MainWindow.SelectedIndexChanged))
            {
                ActivatePage(pages[Model.SelectedIndex]);
            }
        });
    }

    private void UpdatePagesByModel()
    {
        // Строим мап "Модель -> Контроллер", чтобы пары в нём шли в том же порядке,
        // в каком идут в модели. Это позволит учесть в том числе и случаи, когда
        // новая страница добавлена в середину Model.Pages.
        var map = Model.Pages.ToDictionary(mPage => mPage, _ => (c_Page?)null);

        // Записываем в мап контроллеры, которые уже созданы ранее.
        foreach (var cPage in pages)
        {
            map[cPage.Model] = cPage;
        }

        // Таким образом в мапе теперь пустое Value только для добавленных моделей.
        // Нужно для них создать View и Controller'ы.
        var modelsToAdd = map
            .Where(pair => pair.Value == null)
            .Select(pair => pair.Key)
            .ToArray();
        foreach (var mPage in modelsToAdd)
        {
            var cPage = new c_Page
            {
                View = new v_Page { Visible = false },
                Model = mPage
            };
            map[mPage] = cPage;
        }

        // Запоминаем новый список контроллеров с учётом добавленных страниц.
        pages = map.Values.Select(cPage => cPage!).ToArray();
    }

    private void ActivatePage(c_Page controllerToActivate)
    {
        var focused = GetFocusedPage();
        if (focused != null) focused.View.Visible = false;

        var view = controllerToActivate.View;
        view.Parent = View;
        view.Visible = true;
        view.Dock = DockStyle.Fill;
        view.BringToFront();

        View.tbPages.SelectedIndex = Model.SelectedIndex;

        controllerToActivate.SetFocus();
    }

    private c_Page? GetFocusedPage()
    {
        return pages.FirstOrDefault(cPage => cPage.View.ContainsFocus);
    }

    private void View_KeyDown(object? sender, KeyEventArgs e)
    {
        ((IKeyHandler)this).HandleKeyWithNested(e);
    }

    private void TbPages_OnSelectedIndexChanged(object? sender, EventArgs e)
    {
        if (modelSuppressor.Suppress) return;
        Model.SelectedIndex = View.tbPages.SelectedIndex;
    }

    private void TmrTimer_Tick(object? sender, EventArgs e)
    {
        View.Text = DateTime.Now.TimeOfDay.ToString("hh\\:mm\\:ss");
    }

    private void Button1_Click(object? sender, EventArgs e)
    {
    }

    #region IKeyHandler implementation

    public bool HandleKey(KeyEventArgs e)
    {
        if (!View.ContainsFocus) return false;

        var result = true;
        if (e is { KeyCode: Keys.T, Control: true })
            Model.AddPage();
        else if (e is { KeyCode: Keys.Tab, Control: true })
            Model.NextPage(!e.Shift);
        else if (e is { KeyCode: Keys.F12 })
        {
            View.tmrTimer.Enabled = !View.tmrTimer.Enabled;
            if (!View.tmrTimer.Enabled) View.Text = "Developer Notepad";
        }
        else
            result = false;

        return result;
    }

    public IKeyHandler[] NestedKeyHandlers => pages.Cast<IKeyHandler>().ToArray();

    #endregion
}
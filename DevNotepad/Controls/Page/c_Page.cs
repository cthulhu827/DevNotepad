using DevNotepad.Controls.WorkArea;
using DevNotepad.Infrastructure;
using Framework.MVC;

namespace DevNotepad.Controls.Page;

public class c_Page : MVC_Controller<m_Page, v_Page>, IKeyHandler
{
    private IList<c_WorkArea> workAreas = new List<c_WorkArea>();

    protected override void DoConnectModel()
    {
        base.DoConnectModel();

        ApplyModelChanges(null);
    }

    protected override void OnModelChanged(int[] changeCodes)
    {
        ApplyModelChanges(changeCodes.ToEnums<p_Page>());
    }

    private void ApplyModelChanges(p_Page[]? changes)
    {
        changes ??= Enums.Values<p_Page>();

        modelSuppressor.Exec(() =>
        {
            if (changes.Contains(p_Page.WorkAreasListChanged))
            {
                UpdateWorkAreasByModel();
            }
        });
    }

    private void UpdateWorkAreasByModel()
    {
        // Определяем удалённые workarea, т.е. те, которые ещё присутствуют
        // во View и в контроллере, но уже отсутствуют в модели.
        var removedWorkAreas = workAreas
            .Where(cWorkArea => !Model.WorkAreas.Contains(cWorkArea.Model))
            .ToArray();

        // Строим мап "Модель -> Контроллер", чтобы пары в нём шли в том же порядке,
        // в каком идут в модели. Это позволит учесть в том числе и случаи, когда
        // новая workarea добавлена в середину Model.WorkAreas.
        var map = Model.WorkAreas.ToDictionary(mWorkArea => mWorkArea, _ => (c_WorkArea?)null);

        // Записываем в мап контроллеры, которые уже созданы ранее.
        foreach (var cWorkArea in workAreas.Except(removedWorkAreas))
        {
            map[cWorkArea.Model] = cWorkArea;
        }

        // Таким образом в мапе теперь пустое Value только для добавленных моделей.
        // Нужно для них создать View и Controller'ы.
        var modelsToAdd = map
            .Where(pair => pair.Value == null)
            .Select(pair => pair.Key)
            .ToArray();
        c_WorkArea? lastAdded = null;
        foreach (var mWorkArea in modelsToAdd)
        {
            lastAdded = new c_WorkArea
            {
                View = new v_WorkArea { Parent = View },
                Model = mWorkArea
            };
            map[mWorkArea] = lastAdded;
        }

        // Запоминаем новый список контроллеров с учётом добавленных страниц.
        workAreas = map.Values.Select(cWorkArea => cWorkArea!).ToArray();

        // Удаляем View для удалённых workarea.
        foreach (var cWorkArea in removedWorkAreas)
        {
            View.Controls.Remove(cWorkArea.View);
        }

        // Переразмещаем оставшиеся вьюхи.
        AdjusthWorkAreas();

        // Фокусируем последнюю добавленую workarea. В большинстве случаев
        // добавляется одна workarea, она и будет сфокусирована.
        // Неплохо бы также перемещать фокус после удаления workarea в следующую за ней
        // или в предыдущую, но пока это кажется избыточным усложнением кода.
        lastAdded?.SetFocus();
    }

    private void AdjusthWorkAreas()
    {
        var allViews = workAreas.Select(controller => controller.View).ToArray();

        for (int i = 0; i < allViews.Length; i++)
        {
            allViews[i].Dock = i == allViews.Length - 1 ? DockStyle.Fill : DockStyle.Top;
        }
        allViews.LastOrDefault()?.BringToFront();

        var unresizedViews = allViews.Where(view => !view.ManualResized).ToArray();
        if (unresizedViews.Length < 2) return;

        var resizedViewsHeight = allViews.Where(view => view.ManualResized).Sum(view => view.Height);
        var unresizedViewHeight = (View.ClientSize.Height - resizedViewsHeight) / unresizedViews.Length;
        foreach (var view in unresizedViews.Take(unresizedViews.Length - 1))
        {
            view.Height = unresizedViewHeight;
        }
    }

    private int GetFocusedWorkAreaIdx()
    {
        for (int i = 0; i < workAreas.Count; i++)
            if (workAreas[i].View.ContainsFocus)
                return i;
        return -1;
    }

    private void FocusWorkArea(int idx, int focusedIdx)
    {
        if (idx >= 0 && idx < workAreas.Count && idx != focusedIdx)
            workAreas[idx].SetFocus();
    }

    public void SetFocus()
    {
        workAreas.LastOrDefault()?.SetFocus();
    }

    #region IKeyHandler implementation

    public bool IsFocused()
    {
        return View.ContainsFocus;
    }

    public bool HandleKey(KeyEventArgs e)
    {
        var result = true;
        if (e is { KeyCode: Keys.N, Control: true })
        {
            var activeWorkArea = workAreas.FirstOrDefault(c => c.View.ContainsFocus);
            var newModel = activeWorkArea?.ModelNullable?.Copy(e.Shift, e.Alt);
            Model.AddWorkArea(newModel);
        }
        else if (e is { KeyCode: Keys.Down, Control: true })
        {
            var focusedIdx = GetFocusedWorkAreaIdx();
            FocusWorkArea(focusedIdx + 1, focusedIdx);
        }
        else if (e is { KeyCode: Keys.Up, Control: true })
        {
            var focusedIdx = GetFocusedWorkAreaIdx();
            FocusWorkArea(focusedIdx - 1, focusedIdx);
        }
        else if (e is { KeyCode: >= Keys.NumPad1 and <= Keys.NumPad9, Control: true })
            FocusWorkArea(e.KeyCode - Keys.NumPad1, GetFocusedWorkAreaIdx());
        else if (e is { KeyCode: Keys.W, Control: true })
        {
            Model.RemoveWorkArea(GetFocusedWorkAreaIdx());
        }
        else
            result = false;

        return result;
    }

    public IKeyHandler[] NestedKeyHandlers => workAreas.Cast<IKeyHandler>().ToArray();

    #endregion
}
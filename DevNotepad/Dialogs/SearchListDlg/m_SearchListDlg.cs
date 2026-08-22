using Framework.Domain;
using Framework.MVC;
using Framework.UI;

namespace DevNotepad.Dialogs.SearchListDlg;

public class m_SearchListDlg<TViewModel> : MVC_Model
    where TViewModel : ViewModel
{
    public const int SearchTextChanged = 1;
    public const int SelectionChanged = 2;

    public static readonly int[] AllChanges = { SearchTextChanged, SelectionChanged };

    private string searchText = string.Empty;
    private int selectedId;

    public m_SearchListDlg(TViewModel[] all)
    {
        All = all;
        ApplySearchText();
    }

    public TViewModel[] All { get; }

    public string SearchText
    {
        get => searchText;
        set
        {
            if (searchText == value) return;

            searchText = value;
            ApplySearchText();
            NotifyChanged(SearchTextChanged); // todo: во всех моделях избегать множественных нотификаций
        }
    }

    public int SelectedId
    {
        get => selectedId;
        set
        {
            if (selectedId == value) return;

            selectedId = value;
            NotifyChanged(SelectionChanged);
        }
    }

    public IDataSource<TViewModel> Filtered { get; } = new DataSource<TViewModel>();

    private void ApplySearchText()
    {
        // Сбрасываем выбранный элемент, т.к. далее будет очистка списка,
        // и он не будет иметь смысла. Вместо этого можно ловить событие
        // изменения DataSource и проверять, не стал ли он пустым.
        SelectedId = Entity.NullId;

        Filtered.BeginUpdate();
        try
        {
            Filtered.Clear();
            Filtered.AddItems(All.Where(SearchPredicate));
        }
        finally
        {
            Filtered.EndUpdate();
        }

        SelectedId = Filtered.Count == 0 || string.IsNullOrWhiteSpace(searchText)
            ? Entity.NullId
            : Filtered.Items.First().Id; // todo: обернуть, чтобы была только одна нотификация
    }

    private bool SearchPredicate(TViewModel viewModel)
    {
        return string.IsNullOrWhiteSpace(searchText) ||
               viewModel.Text.Contains(searchText, StringComparison.InvariantCultureIgnoreCase);
    }
}
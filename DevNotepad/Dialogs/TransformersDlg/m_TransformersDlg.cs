using Framework.Domain;
using Framework.MVC;

namespace DevNotepad.Dialogs.TransformersDlg;

public class m_TransformersDlg : MVC_Model
{
    public const int SearchTextChanged = 1;
    public const int SelectionChanged = 2;

    public static readonly int[] AllChanges = { SearchTextChanged, SelectionChanged };

    private string searchText = string.Empty;
    private int selectedId;

    public m_TransformersDlg(VM_TransformerForDlg[] allTransformers)
    {
        AllTransformers = allTransformers;
        ApplySearchText();
    }

    public VM_TransformerForDlg[] AllTransformers { get; }

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

    public IDataSource<VM_TransformerForDlg> FilteredTransformers { get; } = new DataSource<VM_TransformerForDlg>();

    private void ApplySearchText()
    {
        // Сбрасываем выбранный элемент, т.к. далее будет очистка списка,
        // и он не будет иметь смысла. Вместо этого можно ловить событие
        // изменения DataSource и проверять, не стал ли он пустым.
        SelectedId = Entity.NullId;

        FilteredTransformers.BeginUpdate();
        try
        {
            FilteredTransformers.Clear();
            FilteredTransformers.AddItems(AllTransformers.Where(SearchPredicate));
        }
        finally
        {
            FilteredTransformers.EndUpdate();
        }

        SelectedId = FilteredTransformers.Count == 0 || string.IsNullOrWhiteSpace(searchText)
            ? Entity.NullId
            : FilteredTransformers.Items.First().Id; // todo: обернуть, чтобы была только одна нотификация
    }

    private bool SearchPredicate(VM_TransformerForDlg transformerForDlg)
    {
        return string.IsNullOrWhiteSpace(searchText) ||
               transformerForDlg.Text.Contains(searchText, StringComparison.InvariantCultureIgnoreCase);
    }
}
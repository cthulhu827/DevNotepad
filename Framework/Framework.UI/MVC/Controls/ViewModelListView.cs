using System.Windows.Forms;

namespace Framework.UI
{
    public delegate void FillVirtualItemDelegate(ListViewItem item, ViewModel viewModel);

    public class ViewModelListView : DataSourceListView<ViewModel>
    {
        public event FillVirtualItemDelegate OnFillVirtualItem;

        protected override void FillVirtualItem(ListViewItem item, ViewModel viewModel)
        {
            if (OnFillVirtualItem == null)
            {
                base.FillVirtualItem(item, viewModel);
            }
            else
            {
                OnFillVirtualItem(item, viewModel);
            }
        }
    }
}
using Framework.AppInfrastructure;
using Framework.MVC;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Framework.UI
{
    public class DataSourceListView<T> : FrameworkListView where T : ViewModel
    {
        private IObservableCollection<T> dataSource = DataSourceFactory.CreateNull<T>();

        private ISelection<T> selection;

        public DataSourceListView()
            : base()
        {
            VirtualMode = true;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IObservableCollection<T> DataSource
        {
            get
            {
                return dataSource;
            }
            set
            {
                var domainListener = dataSource as IDomainListener;
                if (domainListener != null)
                {
                    domainListener.StopListeningDomain();
                }

                StopListeningDataSource();
                ClearUi();

                dataSource = value ?? DataSourceFactory.CreateNull<T>();

                UpdateUi();
                StartListeningDataSource();

                domainListener = dataSource as IDomainListener;
                if (domainListener != null)
                {
                    domainListener.StartListeningDomain();
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ISelection<T> Selection
        {
            get { return selection ?? (selection = DoInitSelection()); }
        }

        public void ShowItem(T viewModel)
        {
            SelectedIndices.Clear();
            if (viewModel == null)
            {
                FocusedItem = null;
                return;
            }

            for (int i = 0; i < dataSource.Count; i++)
            {
                if (dataSource[i].Id == viewModel.Id)
                {
                    ShowItem(i);
                    break;
                }
            }
        }

        protected virtual ISelection<T> DoInitSelection()
        {
            return new DataSourceListViewSelection<T>(this);
        }

        protected virtual void FillVirtualItem(ListViewItem item, T viewModel)
        {
            item.Text = viewModel.Text;
        }

        protected override void OnRetrieveVirtualItem(RetrieveVirtualItemEventArgs e)
        {
            base.OnRetrieveVirtualItem(e);

            if (e.Item != null)
            {
                return;
            }

            e.Item = new ListViewItem();
            FillVirtualItem(e.Item, DataSource[e.ItemIndex]);
        }

        private void On_NEListChanged(NEListChanged evnt)
        {
            UpdateUi();
        }

        private void On_NEListChangedContents(NEListChangedContents evnt)
        {
            if (evnt.ItemIndex != -1)
            {
                RedrawItems(evnt.ItemIndex, evnt.ItemIndex, false);
            }
            else
            {
                Invalidate();
            }
        }

        private void On_NEListClear(NEListClear evnt)
        {
            ClearUi();
        }

        private void On_NEListItemHighlight(NEListItemHighlight evnt)
        {
            switch (evnt.HighlightType)
            {
                case NEListItemHighlight.HighlightId:
                    for (int i = 0; i < dataSource.Count; i++)
                    {
                        if (dataSource[i].Id == evnt.Item)
                        {
                            ShowItem(i);
                            break;
                        }
                    }
                    break;
                case NEListItemHighlight.HighlightIndex:
                    ShowItem(evnt.Item);
                    break;
            }
        }

        private void StartListeningDataSource()
        {
            dataSource.MessageBus.Sign<NEListChanged>(On_NEListChanged);
            dataSource.MessageBus.Sign<NEListChangedContents>(On_NEListChangedContents);
            dataSource.MessageBus.Sign<NEListClear>(On_NEListClear);
            dataSource.MessageBus.Sign<NEListItemHighlight>(On_NEListItemHighlight);
        }

        private void UpdateUi()
        {
            VirtualListSize = dataSource.Count;
            SelectedIndices.Clear();
            Invalidate();
        }

        private void ClearUi()
        {
            VirtualListSize = 0;
            Invalidate();
        }

        private void StopListeningDataSource()
        {
            dataSource.MessageBus.UnsignObject(this);
        }
    }
}

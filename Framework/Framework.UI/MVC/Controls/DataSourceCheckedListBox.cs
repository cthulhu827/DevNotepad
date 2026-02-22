using Framework.AppInfrastructure;
using Framework.MVC;
using System.ComponentModel;
using System.Windows.Forms;

namespace Framework.UI
{
    public class DataSourceCheckedListBox<T> : CheckedListBox where T : ViewModel
    {
        public DataSourceCheckedListBox()
            : base()
        {
            CheckOnClick = true;
        }

        private IDataSource<T> dataSource = DataSourceFactory.CreateNull<T>();

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new IDataSource<T> DataSource
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
                ClearUI();

                dataSource = value;
                if (dataSource == null)
                {
                    dataSource = DataSourceFactory.CreateNull<T>();
                }

                UpdateUI();
                StartListeningDataSource();

                domainListener = dataSource as IDomainListener;
                if (domainListener != null)
                {
                    domainListener.StartListeningDomain();
                }
            }
        }

        private void StartListeningDataSource()
        {
            dataSource.MessageBus.Sign<NEListChanged>(On_NEListChanged);
            dataSource.MessageBus.Sign<NEListChangedContents>(On_NEListChangedContents);
            dataSource.MessageBus.Sign<NEListClear>(On_NEListClear);
        }

        private void UpdateUI()
        {
            BeginUpdate();
            try
            {
                Items.Clear();
                foreach (var vm in dataSource.Items)
                {
                    Items.Add(vm.Text);
                }
            }
            finally
            {
                EndUpdate();
            }
        }

        private void ClearUI()
        {
            Items.Clear();
        }

        private void StopListeningDataSource()
        {
            dataSource.MessageBus.UnsignObject(this);
        }

        private void On_NEListChanged(NEListChanged evnt)
        {
            UpdateUI();
        }

        private void On_NEListChangedContents(NEListChangedContents evnt)
        {
            if (evnt.ItemIndex != -1)
            {
                Items[evnt.ItemIndex] = dataSource[evnt.ItemIndex].Text;
            }
            else
            {
                UpdateUI();
            }
        }

        private void On_NEListClear(NEListClear evnt)
        {
            ClearUI();
        }

        private IMultipleSelection selection = null;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IMultipleSelection Selection
        {
            get
            {
                if (selection == null)
                {
                    selection = DoInitSelection();
                }
                return selection;
            }
        }

        protected virtual IMultipleSelection DoInitSelection()
        {
            return new DataSourceCheckedListBoxSelection<T>(this);
        }
    }
}

using Framework.AppInfrastructure;
using Framework.MVC;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Framework.UI
{
    public class DataSourceComboBox<T> : ComboBox where T : ViewModel
    {
        private IDataSource<T>? dataSource = DataSourceFactory.CreateNull<T>();

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new IDataSource<T>? DataSource
        {
            get => dataSource;
            set
            {
                if (dataSource is IDomainListener domainListener)
                {
                    domainListener.StopListeningDomain();
                }

                StopListeningDataSource();
                ClearUi();

                dataSource = value ?? DataSourceFactory.CreateNull<T>();

                UpdateUi();
                StartListeningDataSource();

                if (dataSource is IDomainListener domainListener1)
                {
                    domainListener1.StartListeningDomain();
                }
            }
        }

        private ISelection<T>? selection;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ISelection<T> Selection => selection ??= DoInitSelection();

        protected virtual ISelection<T> DoInitSelection()
        {
            return new DataSourceComboBoxSelection<T>(this);
        }

        private void On_NEListChanged(NEListChanged evnt)
        {
            UpdateUi();
        }

        private void On_NEListChangedContents(NEListChangedContents evnt)
        {
            if (evnt.ItemIndex == SelectedIndex)
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
                    SelectedItem = dataSource?.Items.FirstOrDefault(item => item.Id == evnt.Item);
                    break;
                case NEListItemHighlight.HighlightIndex:
                    SelectedIndex = evnt.Item;
                    break;
            }
        }

        private void StartListeningDataSource()
        {
            if (dataSource == null) return;

            dataSource.MessageBus.Sign<NEListChanged>(On_NEListChanged);
            dataSource.MessageBus.Sign<NEListChangedContents>(On_NEListChangedContents);
            dataSource.MessageBus.Sign<NEListClear>(On_NEListClear);
            dataSource.MessageBus.Sign<NEListItemHighlight>(On_NEListItemHighlight);
        }

        private void UpdateUi()
        {
            Items.Clear();
            if (dataSource != null) Items.AddRange(dataSource.Items.Cast<object>().ToArray());
        }

        private void ClearUi()
        {
            Items.Clear();
        }

        private void StopListeningDataSource()
        {
            dataSource?.MessageBus.UnsignObject(this);
        }
    }
}
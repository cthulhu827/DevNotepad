using Framework.AppInfrastructure;
using Framework.MVC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Framework.UI
{
    public class DataSourceComboBox<T> : ComboBox where T : ViewModel
    {
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

        private ISelection<T> selection = null;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ISelection<T> Selection
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

        protected virtual ISelection<T> DoInitSelection()
        {
            return new DataSourceComboBoxSelection<T>(this);
        }

        private void On_NEListChanged(NEListChanged evnt)
        {
            UpdateUI();
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
            ClearUI();
        }

        private void On_NEListItemHighlight(NEListItemHighlight evnt)
        {
            switch (evnt.HighlightType)
            {
                case NEListItemHighlight.HighlightId:
                    SelectedItem = dataSource.Items.FirstOrDefault(item => item.Id == evnt.Item);
                    break;
                case NEListItemHighlight.HighlightIndex:
                    SelectedIndex = evnt.Item;
                    break;
                default:
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

        private void UpdateUI()
        {
            Items.Clear();
            Items.AddRange(dataSource.Items.ToArray());
        }

        private void ClearUI()
        {
            Items.Clear();
        }

        private void StopListeningDataSource()
        {
            dataSource.MessageBus.UnsignObject(this);
        }
    }
}

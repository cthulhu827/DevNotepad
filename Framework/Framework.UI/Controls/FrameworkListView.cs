using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace Framework.UI
{
    [ToolboxItem(true)]
    public class FrameworkListView : ListView
    {
        public FrameworkListView()
            : base()
        {
            View = View.Details;
            FullRowSelect = true;
            HideSelection = false;
        }

        [DefaultValue(true)]
        public new bool FullRowSelect
        {
            get { return base.FullRowSelect; }
            set { base.FullRowSelect = value; }
        }

        [DefaultValue(false)]
        public new bool HideSelection
        {
            get { return base.HideSelection; }
            set { base.HideSelection = value; }
        }

        [DefaultValue(View.Details)]
        public new View View
        {
            get { return base.View; }
            set { base.View = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedIndex
        {
            get
            {
                if (SelectedIndices.Count == 0)
                    return -1;
                else
                    return SelectedIndices[0];
            }
        }

        public ListViewItem SelectedItem
        {
            get
            {
                int Idx = SelectedIndex;
                if (Idx == -1)
                    return null;
                else
                    return Items[Idx];
            }
        }

        public event EventHandler SelectionChanged;

        public void ShowItem(int index)
        {
            SelectedIndices.Clear();
            SelectedIndices.Add(index);
            EnsureVisible(index);
            FocusedItem = Items[index];
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            RaiseSelectionChanged();
        }

        protected override void OnVirtualItemsSelectionRangeChanged(ListViewVirtualItemsSelectionRangeChangedEventArgs e)
        {
            base.OnVirtualItemsSelectionRangeChanged(e);
            if (SelectedIndices.Count > 0)
            {
                RaiseSelectionChanged();
            }
        }

        private void RaiseSelectionChanged()
        {
            if (SelectionChanged != null)
            {
                SelectionChanged(this, EventArgs.Empty);
            }
        }
    }
}

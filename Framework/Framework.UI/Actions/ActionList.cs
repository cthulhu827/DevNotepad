using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Framework.UI
{
    public class ActionList : List<UIAction>
    {
        private void OnIdle(object sender, EventArgs args)
        {
            foreach (UIAction Action in this) Action.Update();
        }

        private EventHandler appOnIdle;

        public ActionList()
            : base()
        {
            appOnIdle = new EventHandler(OnIdle);
        }

        public void ProcessUpdates(bool process)
        {
            if (process)
                Application.Idle += appOnIdle;
            else
                Application.Idle -= appOnIdle;
        }

        public void AddAction(UIAction action)
        {
            action.ActionList = this;
        }

        public void RemoveAction(UIAction action)
        {
            action.ActionList = null;
        }

        public ImageList Images { get; set; }

        public void Reset()
        {
            foreach (UIAction actn in this) actn.UnsignControls();
            Clear();
        }
    }
}

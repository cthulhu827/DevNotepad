using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Framework.UI
{
    public class ActionLink_ToolStripButton : ActionLink_Base<ToolStripButton>, IActionLink
    {
        public ActionLink_ToolStripButton(UIAction action, ToolStripButton control) : base(action, control) { }

        public void SetVisible()
        {
            Control.Visible = Action.Visible;
        }

        public void SetEnabled()
        {
            Control.Enabled = Action.Enabled;
        }

        public void SetCaption()
        {
            Control.Text = Action.Caption;
        }

        public void SetImageIndex()
        {
            if ((Action.ActionList == null) || (Action.ActionList.Images == null) || (Action.ImageIndex == -1))
                Control.Image = null;
            else
                Control.Image = Action.ActionList.Images.Images[Action.ImageIndex];
        }

        private void OnClick(object sender, EventArgs e)
        {
            Action.Execute();
        }

        private EventHandler onClick;

        public void SetExecuteHandler(bool set)
        {
            if (onClick == null) onClick = new EventHandler(OnClick);

            if (set)
                Control.Click += onClick;
            else
                Control.Click -= onClick;
        }
    }
}

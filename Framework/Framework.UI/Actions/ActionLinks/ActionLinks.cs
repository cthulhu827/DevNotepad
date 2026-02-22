using Framework.AppInfrastructure;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Framework.UI
{
    public class ActionLinks
    {
        private static readonly TypeMapper map = new TypeMapper(false);

        static ActionLinks()
        {
            map.Register<ToolStripButton, ActionLink_ToolStripButton>();
            map.Register<ToolStripSplitButton, ActionLink_ToolStripSplitButton>();
            map.Register<ToolStripMenuItem, ActionLink_ToolStripMenuItem>();
            map.Register<Button, ActionLink_Button>();
            map.Register<NonFocusableButton, ActionLink_NonFocusableButton>();
        }

        internal static IActionLink Create(UIAction action, object control)
        {
            return Activator.CreateInstance(map.Get(control.GetType()), action, control) as IActionLink;
        }
    }
}

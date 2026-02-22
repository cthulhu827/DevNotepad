using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.UI
{
    public abstract class ActionLink_Base<T> where T : class
    {
        public UIAction Action { get; private set; }
        public T Control { get; private set; }

        public ActionLink_Base(UIAction action, T control)
        {
            Action = action;
            Control = control;
        }
    }
}

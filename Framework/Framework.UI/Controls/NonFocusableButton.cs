using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Framework.UI
{
    public class NonFocusableButton : Button
    {
        public NonFocusableButton()
            : base()
        {
            SetStyle(ControlStyles.Selectable, false);
        }
    }
}

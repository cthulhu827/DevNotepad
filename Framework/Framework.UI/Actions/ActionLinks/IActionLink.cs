using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.UI
{
    public interface IActionLink
    {
        void SetVisible();
        void SetEnabled();
        void SetCaption();
        void SetImageIndex();
        void SetExecuteHandler(bool set);
    }
}

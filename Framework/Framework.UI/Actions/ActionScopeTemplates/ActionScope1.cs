using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.UI
{
    public class ActionScope<T1> : ActionScope
    {
        private readonly Func<T1> getter1;

        public ActionScope(Func<T1> getter1)
        {
            this.getter1 = getter1;
        }

        public T1 Prop1
        {
            get { return getter1(); }
        }
    }
}

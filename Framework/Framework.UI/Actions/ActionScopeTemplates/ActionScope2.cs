using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.UI
{
    public class ActionScope<T1, T2> : ActionScope<T1>
    {
        private readonly Func<T2> getter2;

        public ActionScope(Func<T1> getter1, Func<T2> getter2)
            : base(getter1)
        {
            this.getter2 = getter2;
        }

        public T2 Prop2
        {
            get { return getter2(); }
        }
    }
}

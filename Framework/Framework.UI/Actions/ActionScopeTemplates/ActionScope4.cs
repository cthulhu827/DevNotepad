using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.UI
{
    public class ActionScope<T1, T2, T3, T4> : ActionScope<T1, T2, T3>
    {
        private readonly Func<T4> getter4;

        public ActionScope(Func<T1> getter1, Func<T2> getter2, Func<T3> getter3, Func<T4> getter4)
            : base(getter1, getter2, getter3)
        {
            this.getter4 = getter4;
        }

        public T4 Prop4
        {
            get { return getter4(); }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.UI
{
    public class ActionScope<T1, T2, T3> : ActionScope<T1, T2>
    {
        private readonly Func<T3> getter3;

        public ActionScope(Func<T1> getter1, Func<T2> getter2, Func<T3> getter3)
            : base(getter1, getter2)
        {
            this.getter3 = getter3;
        }

        public T3 Prop3
        {
            get { return getter3(); }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.UI
{
    public class ActionScope<T1, T2, T3, T4, T5> : ActionScope<T1, T2, T3, T4>
    {
        private readonly Func<T5> getter5;

        public ActionScope(Func<T1> getter1, Func<T2> getter2, Func<T3> getter3, Func<T4> getter4, Func<T5> getter5)
            : base(getter1, getter2, getter3, getter4)
        {
            this.getter5 = getter5;
        }

        public T5 Prop5
        {
            get { return getter5(); }
        }
    }
}

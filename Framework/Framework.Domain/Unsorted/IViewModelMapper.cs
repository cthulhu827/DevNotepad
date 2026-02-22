using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Domain
{
    public interface IViewModelMapper<in TIn, out TOut> where TIn : class
    {
        TOut Map(TIn entity);
    }
}

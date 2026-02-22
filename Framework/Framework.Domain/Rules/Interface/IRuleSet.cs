using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Domain
{
    public interface IRuleSet<T> : IRule<T>
    {
        IEnumerable<IRule<T>> NestedRules { get; }
        void Add(IRule<T> rule);
        IRule<T> LastRule();
        IRule<T> ById(int ruleId);
    }
}

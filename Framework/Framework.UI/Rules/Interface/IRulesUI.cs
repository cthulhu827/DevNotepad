using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Domain;

namespace Framework.UI
{
    public interface IRulesUI<T>
    {
        void AddLink(int ruleId, IRuleUILink<T> link);
        void Unsign();
        IRuleSet<T> Rules { get; }
    }
}

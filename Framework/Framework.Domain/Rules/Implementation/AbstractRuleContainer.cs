using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Domain
{
    abstract class AbstractRuleContainer<T> : AbstractRule<T>, IRuleSet<T>
    {
        protected AbstractRuleContainer(IRuleSet<T> owner) : base(owner)
        {
        }

        private readonly IList<IRule<T>> nestedRules = new List<IRule<T>>();

        public IEnumerable<IRule<T>> NestedRules { get { return nestedRules; } }

        public void Add(IRule<T> rule)
        {
            nestedRules.Add(rule);
        }

        public IRule<T> LastRule()
        {
            return nestedRules.Count == 0 ? null : nestedRules.Last();
        }

        public IRule<T> ById(int ruleId)
        {
            foreach (var rule in nestedRules)
            {
                if (rule.Id == ruleId)
                    return rule;

                var ruleSet = rule as IRuleSet<T>;
                if (ruleSet != null)
                {
                    var childRule = ruleSet.ById(ruleId);
                    if (childRule != null)
                        return childRule;
                }
            }
            return null;
        }
    }
}

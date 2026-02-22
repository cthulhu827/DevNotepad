using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Domain
{
    class Rule_Container_List<T> : AbstractRuleContainer<T>
    {
        public Rule_Container_List(IRuleSet<T> owner) : base(owner) { }

        public override RuleState Validate(T objectToValidate, IRuleSet<T> brokenRules)
        {
            var result = RuleState.Valid;

            foreach (var rule in NestedRules)
            {
                rule.Validate(objectToValidate, brokenRules);
                if (rule.State < result) result = rule.State;
            }

            State = result;
            if ((brokenRules != null) && (State.IsNotValid())) brokenRules.Add(this);

            return State;
        }
    }
}

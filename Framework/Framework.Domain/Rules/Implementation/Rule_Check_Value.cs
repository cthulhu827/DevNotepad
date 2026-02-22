using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Domain
{
    class Rule_Check_Value<T, TValue> : AbstractRule<T>
    {
        private readonly Func<T, TValue> getValueDelegate;
        private readonly Func<TValue, RuleState> validateDelegate;

        public Rule_Check_Value(IRuleSet<T> owner, Func<T, TValue> getValueDelegate, Func<TValue, RuleState> validateDelegate)
            : base(owner)
        {
            this.getValueDelegate = getValueDelegate;
            this.validateDelegate = validateDelegate;
        }

        public override RuleState Validate(T objectToValidate, IRuleSet<T> brokenRules)
        {
            TValue value = getValueDelegate(objectToValidate);
            State = validateDelegate(value);

            if ((brokenRules != null) && (State.IsNotValid())) brokenRules.Add(this);

            return State;
        }
    }
}

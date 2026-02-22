using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Domain
{
    abstract class AbstractRule<T> : IRule<T>
    {
        protected AbstractRule(IRuleSet<T> owner)
        {
            Owner = owner;
            if (owner != null)
            {
                owner.Add(this);
            }
        }

        private RuleState state = RuleState.None;

        public RuleState State
        {
            get
            {
                return state;
            }
            set
            {
                if (state != value)
                {
                    var oldState = state;
                    state = value;
                    if (OnStateChange != null)
                        OnStateChange(this, oldState, state);
                }
            }
        }

        public abstract RuleState Validate(T objectToValidate, IRuleSet<T> brokenRules);

        public IRuleSet<T> Owner { get; private set; }
        public string InvalidMessage { get; set; }
        public int Id { get; set; }
        public OnRuleStateChange<T> OnStateChange { get; set; }
    }
}

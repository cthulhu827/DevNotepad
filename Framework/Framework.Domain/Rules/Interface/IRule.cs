using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Domain
{
    public delegate void OnRuleStateChange<T>(IRule<T> rule, RuleState oldState, RuleState newState);

    public interface IRule<T>
    {
        IRuleSet<T> Owner { get; }
        RuleState State { get; }
        RuleState Validate(T objectToValidate, IRuleSet<T> brokenRules);
        int Id { get; set; }
        string InvalidMessage { get; set; }
        OnRuleStateChange<T> OnStateChange { get; set; }
    }
}

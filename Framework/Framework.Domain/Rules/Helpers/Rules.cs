using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Domain
{
    public static class Rules
    {
        public static void Raise<T>(IRuleSet<T> brokenRules)
        {
            throw new ERulesValidation<T>(brokenRules);
        }

        public static IRuleSet<T> CreateBrokenRules<T>()
        {
            return new Rule_Container_List<T>(null);
        }

        public static IRuleSet<T> CreateRuleList<T>()
        {
            return new Rule_Container_List<T>(null);
        }

        public static bool IsValid(this RuleState state)
        {
            return state == RuleState.Valid;
        }

        public static bool IsNotValid(this RuleState state)
        {
            return state != RuleState.Valid;
        }

        public static bool IsInvalid(this RuleState state)
        {
            return state == RuleState.Invalid;
        }

        public static void Validate<T>(T objectToValidate, IRuleSet<T> rules)
        {
            var brokenRules = CreateBrokenRules<T>();

            if (rules.Validate(objectToValidate, brokenRules).IsNotValid())
            {
                Raise(brokenRules);
            }
        }
    }
}

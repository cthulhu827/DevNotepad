using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Domain
{
    public class ERulesValidation<T> : AppException
    {
        private static string BuildErrorMessage(IRuleSet<T> brokenRules)
        {
            int idx = 1;
            return "Broken rules:\n" +
                brokenRules.NestedRules
                    .Where(rule => rule.State.IsInvalid())
                    .Where(rule => !string.IsNullOrEmpty(rule.InvalidMessage))
                    .Select(rule => string.Format("{0}. {1}", idx++, rule.InvalidMessage))
                    .Join(";\n");
        }

        public IRuleSet<T> BrokenRules { get; private set; }

        public ERulesValidation(IRuleSet<T> brokenRules)
            : base(BuildErrorMessage(brokenRules))
        {
            BrokenRules = brokenRules;
        }
    }
}

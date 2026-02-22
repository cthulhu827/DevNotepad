using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Domain;

namespace Framework.UI
{
    class RulesUI<T> : IRulesUI<T>
    {
        private struct LinkRec
        {
            public IRule<T> Rule;
            public IRuleUILink<T> Link;
        }

        private IList<LinkRec> FLinks = new List<LinkRec>();

        public IRuleSet<T> Rules { get; private set; }

        public RulesUI(IRuleSet<T> rules)
        {
            this.Rules = rules;
        }

        public void AddLink(int ruleId, IRuleUILink<T> link)
        {
            if (!EntityUtils.IsAssigned(ruleId))
                throw new AppException("Rule ID is not assigned");

            IRule<T> rule = Rules.ById(ruleId);
            if (rule == null)
                throw new AppException(string.Format("Rule ID={0} not found", ruleId));

            rule.OnStateChange += link.OnStateChange;

            FLinks.Add(new LinkRec() { Rule = rule, Link = link });
        }

        public void Unsign()
        {
            foreach (var rec in FLinks)
            {
                rec.Rule.OnStateChange -= rec.Link.OnStateChange;
            }
        }
    }
}

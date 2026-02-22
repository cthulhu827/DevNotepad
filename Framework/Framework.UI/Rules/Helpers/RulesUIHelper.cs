using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Framework.Domain;

namespace Framework.UI
{
    public static class RulesUIHelper
    {
        public static IRulesUI<T> BindLabel<T>(this IRulesUI<T> rulesUi, int ruleId, Label label)
        {
            rulesUi.AddLink(ruleId, new RuleUILink_Label<T>(label));
            return rulesUi;
        }

        public static IRulesUI<T> BindErrorProvider<T>(this IRulesUI<T> rulesUi, int ruleId, Control control)
        {
            rulesUi.AddLink(ruleId, new RuleUILink_ErrorProvider<T>(control));
            return rulesUi;
        }

        public static IRulesUI<T> CreateRulesUI<T>(IRuleSet<T> rules)
        {
            return new RulesUI<T>(rules);
        }
    }
}

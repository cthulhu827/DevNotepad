using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Framework.Domain;

namespace Framework.UI
{
    class RuleUILink_Label<T> : IRuleUILink<T>
    {
        private readonly Label label;

        public RuleUILink_Label(Label label)
        {
            this.label = label;

            this.label.Visible = false;
            this.label.BackColor = Color.Red;
            this.label.ForeColor = Color.White;
        }

        public void OnStateChange(IRule<T> rule, RuleState oldState, RuleState newState)
        {
            label.Visible = rule.State.IsInvalid();
            label.Text = rule.InvalidMessage;
        }
    }
}

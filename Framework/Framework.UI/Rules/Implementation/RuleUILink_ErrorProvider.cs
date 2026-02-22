using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Framework.Domain;

namespace Framework.UI
{
    class RuleUILink_ErrorProvider<T> : IRuleUILink<T>
    {
        private readonly ErrorProvider errorProvider;

        private readonly Control control;

        public RuleUILink_ErrorProvider(Control control)
        {
            errorProvider = new ErrorProvider();
            this.control = control;
        }

        public void OnStateChange(IRule<T> rule, RuleState oldState, RuleState newState)
        {
            switch (newState)
            {
                case RuleState.Invalid:
                    errorProvider.SetError(control, rule.InvalidMessage);
                    break;
                case RuleState.Valid:
                    errorProvider.SetError(control, null);
                    break;
                default:
                    throw new AppException("Unsupported newState value {0}", newState);
            }
        }
    }
}

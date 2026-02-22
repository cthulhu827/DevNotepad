using Framework.Domain;
using Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Framework.MVC
{
    public class ModalDialogController<TModel, TView> : MVC_Controller<TModel, TView>
        where TModel : MVC_Model
        where TView : ModalDialog
    {
        private IRulesUI<TModel> validationRules;

        protected override TView DoCreateView(Control parent)
        {
            return Activator.CreateInstance<TView>();
        }

        public bool ShowDialog(TModel model)
        {
            return ShowDialogEx(model) == DialogResult.OK;
        }

        public DialogResult ShowDialogEx(TModel model)
        {
            CreateView(null);
            try
            {
                Model = model;
                try
                {
                    BindValidation();
                    try
                    {
                        return View.ShowDialog();
                    }
                    finally
                    {
                        UnbindValidation();
                    }
                }
                finally
                {
                    ModelNullable = null;
                }
            }
            finally
            {
                View.Dispose();
                ViewNullable = null;
            }
        }

        private void UnbindValidation()
        {
            if (validationRules != null)
            {
                validationRules.Unsign();
                View.Validator = null;
            }
        }

        private void BindValidation()
        {
            validationRules = GetValidationRules();
            if (validationRules != null)
            {
                View.Validator = ValidateViewInput;
            }
        }

        protected virtual IRulesUI<TModel> GetValidationRules()
        {
            return null;
        }

        private bool ValidateViewInput()
        {
            return validationRules.Rules.Validate(Model, null).IsValid();
        }

        protected virtual void UpdateUi()
        {
            // do nothing
        }

        protected virtual void ClearUi()
        {
            // do nothing
        }

        protected override void DoConnectModel()
        {
            base.DoConnectModel();
            UpdateUi();
        }

        protected override void DoDisconnectModel()
        {
            ClearUi();
            base.DoDisconnectModel();
        }

        protected override void DoConnectView()
        {
            base.DoConnectView();
            ClearUi();
        }
    }
}

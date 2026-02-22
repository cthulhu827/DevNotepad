using Framework.UI;
using System;
using System.Windows.Forms;

namespace Framework.MVC
{
    public class MVC_Controller<TModel, TView> : MVC_Controller_Base<TModel, TView>, IMVC_Controller
        where TModel : MVC_Model
        where TView : Control
    {
        #region Public methods

        public void CreateView(Control parent)
        {
            View = DoCreateView(parent);
        }

        #endregion

        #region Protected methods

        protected virtual TView DoCreateView(Control parent)
        {
            var result = Activator.CreateInstance<TView>();
            result.Parent = parent;
            return result;
        }

        #endregion

        #region IMVC_Controller implementation

        MVC_Model? IMVC_Controller.ModelNullable
        {
            get => ModelNullable;
            set => ModelNullable = value as TModel;
        }

        MVC_Model IMVC_Controller.Model
        {
            get => Model;
            set => Model = (TModel)value;
        }

        Control? IMVC_Controller.ViewNullable
        {
            get => ViewNullable;
            set => ViewNullable = value as TView;
        }

        Control IMVC_Controller.View
        {
            get => View;
            set => View = (TView)value;
        }

        Control IMVC_Controller.CreateView(Control parent)
        {
            CreateView(parent);
            return View;
        }

        #endregion
    }
}

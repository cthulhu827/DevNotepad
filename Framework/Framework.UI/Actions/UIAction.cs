using System.Collections.Generic;
using System;

namespace Framework.UI
{
    public delegate void ActionExecuteEvent(UIAction action);

    public delegate void ActionUpdateEvent(UIAction action);

    public class UIAction
    {
        #region Fields

        private readonly IList<IActionLink> controlLinks = new List<IActionLink>();

        private string caption;

        private bool visible = true;

        private bool enabled;

        private ActionList actionList;

        private readonly Func<ActionScope> getScope = () => null;

        #endregion

        #region Constructors

        public UIAction(ActionScope scope, string caption, int imageIndex)
            : base()
        {
            CheckRequiredScopes(scope);

            getScope = () => scope;
            Caption = caption;
            ImageIndex = imageIndex;
        }

        public UIAction(Func<ActionScope> getScope, string caption, int imageIndex)
            : base()
        {
            this.getScope = getScope;
            Caption = caption;
            ImageIndex = imageIndex;
        }

        #endregion

        #region Properties

        public string Caption
        {
            get { return caption; }
            set
            {
                if (caption == value)
                {
                    return;
                }

                caption = value;
                SetControlsCaptions();
            }
        }

        public int ImageIndex { get; set; }

        public bool Visible
        {
            get { return visible; }
            set
            {
                if (visible == value)
                {
                    return;
                }

                visible = value;
                SetControlsVisible();
            }
        }

        public bool Enabled
        {
            get { return enabled; }
            set
            {
                if (enabled == value)
                {
                    return;
                }

                enabled = value;
                SetControlsEnabled();
            }
        }

        public ActionScope Scope
        {
            get { return getScope == null ? null : getScope(); }
        }

        public ActionList ActionList
        {
            get { return actionList; }
            set
            {
                if (actionList == value)
                {
                    return;
                }

                if (actionList != null)
                {
                    actionList.Remove(this);
                }

                actionList = value;

                if (actionList == null)
                {
                    return;
                }

                actionList.Add(this);
                SetControlsImages();
            }
        }

        public event ActionUpdateEvent OnUpdate;

        public event ActionExecuteEvent OnExecute;

        #endregion

        #region Public methods

        public T? As<T>() where T : class, IAS_Base
        {
            return Scope as T;
        }

        public virtual void Update()
        {
            if (OnUpdate != null)
            {
                OnUpdate(this);
            }
        }

        public virtual void Execute()
        {
            if (OnExecute != null)
            {
                OnExecute(this);
            }
        }

        public void SignControls(params object[] controls)
        {
            foreach (var control in controls)
            {
                var link = ActionLinks.Create(this, control);

                controlLinks.Add(link);

                link.SetCaption();
                link.SetImageIndex();
                link.SetVisible();
                link.SetEnabled();
                link.SetExecuteHandler(true);
            }
        }

        public void UnsignControls()
        {
            foreach (var link in controlLinks)
            {
                link.SetExecuteHandler(false);
            }

            controlLinks.Clear();
        }

        public static ActionUpdateEvent AlwaysEnabled = a => { a.Visible = true; a.Enabled = true; };

        #endregion

        #region Private methods

        private void SetControlsVisible()
        {
            foreach (var link in controlLinks)
            {
                link.SetVisible();
            }
        }

        private void SetControlsEnabled()
        {
            foreach (var link in controlLinks)
            {
                link.SetEnabled();
            }
        }

        private void SetControlsImages()
        {
            foreach (var link in controlLinks)
            {
                link.SetImageIndex();
            }
        }

        private void SetControlsCaptions()
        {
            foreach (var link in controlLinks)
            {
                link.SetCaption();
            }
        }

        private void CheckRequiredScopes(ActionScope scope)
        {
            var attrs = GetType().GetCustomAttributes(typeof(RequiresScopeAttribute), false);

            foreach (var attr in attrs)
            {
                var requiredInterface = ((RequiresScopeAttribute)attr).ScopeInterface;
                scope.CheckSupports(requiredInterface);
            }
        }

        #endregion
    }
}
using Framework.UI;

namespace Framework.MVC
{
    public class MVC_Controller_Base<TModel, TView> where TModel : MVC_Model
    {
        #region Fields

        private TModel? model;

        private TView? view;

        private ActionScope? scope;

        private readonly IReactiveMvcController? reactiveController;

        protected readonly EventSuppressor modelSuppressor = new();

        #endregion

        public MVC_Controller_Base()
        {
            reactiveController = DoCreateReactiveController();
        }

        #region Properties

        public TModel Model
        {
            get => ModelNullable!;
            set => ModelNullable = value;
        }

        public TModel? ModelNullable
        {
            get => model;
            set
            {
                BeforeSetModel(model, value);
                try
                {
                    if (model != null)
                    {
                        model.StopListeningDomain();
                        DoStopListeningModel();

                        DoDisconnectModel();

                        if (reactiveController != null)
                        {
                            reactiveController.SetModel(null);
                        }
                    }

                    model = value;

                    if (model != null)
                    {
                        if (reactiveController != null)
                        {
                            reactiveController.SetModel(model);
                        }

                        DoConnectModel();

                        DoStartListeningModel();
                        model.StartListeningDomain();
                    }
                }
                finally
                {
                    AfterSetModel(model, value);
                }
            }
        }

        public TView View
        {
            get => ViewNullable!;
            set => ViewNullable = value;
        }

        public TView? ViewNullable
        {
            get => view;
            set
            {
                if (view != null)
                {
                    ActionList.ProcessUpdates(false);
                    ActionList.Reset();
                    DoDoneActions();

                    scope = null;

                    DoDisconnectView();

                    if (reactiveController != null)
                    {
                        reactiveController.SetView(null);
                    }
                }

                view = value;

                if (view != null)
                {
                    if (reactiveController != null)
                    {
                        reactiveController.SetView(view);
                    }

                    DoConnectView();

                    scope = DoCreateActionScope();

                    DoInitActions();
                    ActionList.ProcessUpdates(true);
                }
            }
        }

        public virtual ActionScope? Scope => scope;

        public ActionList ActionList { get; } = new();

        #endregion

        #region Public methods

        public virtual void StartListeningDomain()
        {
            // do nothing
        }

        public virtual void StopListeningDomain()
        {
            // do nothing
        }

        #endregion

        #region Protected methods

        protected virtual void DoConnectModel()
        {
            // do nothing
        }

        protected virtual void DoDisconnectModel()
        {
            // do nothing
        }

        protected virtual void DoConnectView()
        {
            // do nothing
        }

        protected virtual void DoDisconnectView()
        {
            // do nothing
        }

        protected virtual void DoInitActions()
        {
            // do nothing
        }

        protected virtual void DoDoneActions()
        {
            // do nothing
        }

        protected virtual void DoStartListeningModel()
        {
            Model.MessageBus
                .Sign<NEModelChanged>(evnt => OnModelChanged(evnt.ChangeCodes))
                .Sign<NENewModel>(evnt => Model = (TModel)evnt.NewModel);
        }

        protected virtual void DoStopListeningModel()
        {
            Model.MessageBus.UnsignObject(this);
        }

        protected virtual void OnModelChanged(int[] changeCodes)
        {
            // do nothing
        }

        protected virtual ActionScope? DoCreateActionScope()
        {
            return null;
        }

        protected virtual IReactiveMvcController? DoCreateReactiveController()
        {
            return null;
        }

        protected virtual void BeforeSetModel(TModel? oldModel, TModel? newModel)
        {
            // do nothing
        }

        protected virtual void AfterSetModel(TModel? oldModel, TModel? newModel)
        {
            // do nothing
        }

        #endregion
    }
}
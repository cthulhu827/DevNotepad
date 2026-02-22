using System;
using Framework.AppInfrastructure;

namespace Framework.MVC
{
    public class NEModelChanged : NotifyEvent
    {
        public int[] ChangeCodes { get; set; } = Array.Empty<int>();
    }

    public class NENewModel : NotifyEvent
    {
        public MVC_Model NewModel { get; private set; }

        public NENewModel(MVC_Model newModel)
        {
            NewModel = newModel;
        }
    }

    public class MVC_Model : IDomainListener
    {
        private readonly IMessageBus messageBus = IoC.Resolve<IMessageBus>();

        public IMessageBus MessageBus => messageBus;

        public void NotifyChanged(params int[] changeCodes)
        {
            messageBus.Notify(new NEModelChanged { ChangeCodes = changeCodes });
        }

        public void NotifyChanged(int changeCode)
        {
            NotifyChanged(new[] { changeCode });
        }

        public virtual void StartListeningDomain()
        {
            // do nothing
        }

        public virtual void StopListeningDomain()
        {
            // do nothing
        }

        protected void NewModel(MVC_Model newModel)
        {
            messageBus.Notify(new NENewModel(newModel));
        }
    }
}

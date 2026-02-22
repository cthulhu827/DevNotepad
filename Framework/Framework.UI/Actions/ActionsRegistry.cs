using System;
using System.Collections.Generic;

namespace Framework.UI
{
    public delegate UIAction CreateActionDelegate_Static(ActionScope scope, params object[] createParams);

    public delegate UIAction CreateActionDelegate_Dynamic(Func<ActionScope> getScope, params object[] createParams);

    public static class ActionsRegistry
    {
        private static IDictionary<int, CreateActionDelegate_Static> staticDelegates = new Dictionary<int, CreateActionDelegate_Static>();

        private static IDictionary<int, CreateActionDelegate_Dynamic> dynamicDelegates = new Dictionary<int, CreateActionDelegate_Dynamic>();

        public static void Register(int actionId, CreateActionDelegate_Static createDelegate)
        {
            if (staticDelegates.ContainsKey(actionId))
                throw new AppException("Action creator for ID={0} already registered", actionId);

            staticDelegates.Add(actionId, createDelegate);
        }

        public static void Register(int actionId, CreateActionDelegate_Dynamic createDelegate)
        {
            if (dynamicDelegates.ContainsKey(actionId))
                throw new AppException("Action creator for ID={0} already registered", actionId);

            dynamicDelegates.Add(actionId, createDelegate);
        }

        public static UIAction ById(int actionId, ActionScope scope, params object[] createParams)
        {
            CreateActionDelegate_Static createDelegate;
            if (staticDelegates.TryGetValue(actionId, out createDelegate))
                return createDelegate(scope, createParams);
            else
                throw new AppException("Action creator for ID={0} is not registred", actionId);
        }

        public static UIAction ById(int actionId, Func<ActionScope> getScope, params object[] createParams)
        {
            CreateActionDelegate_Dynamic createDelegate;
            if (dynamicDelegates.TryGetValue(actionId, out createDelegate))
                return createDelegate(getScope, createParams);
            else
                throw new AppException("Action creator for ID={0} is not registred", actionId);
        }
    }
}

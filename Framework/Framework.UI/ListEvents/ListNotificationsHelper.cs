using Framework.Domain;

namespace Framework.AppInfrastructure
{
    public static class ListNotificationsHelper
    {
        public static void Notify_ListClear(this IMessageBus messageBus)
        {
            messageBus.NotifyNoParams<NEListClear>();
        }

        public static void Notify_ListChanged(this IMessageBus messageBus, NotifyEvent internalEvent)
        {
            var evnt = new NEListChanged { InternalEvent = internalEvent };
            messageBus.Notify(evnt);
        }

        public static void Notify_ListChanged(this IMessageBus messageBus)
        {
            Notify_ListChanged(messageBus, null);
        }

        public static void Notify_ListChangedContents(this IMessageBus messageBus, int itemIndex, NotifyEvent internalEvent)
        {
            var evnt = new NEListChangedContents { InternalEvent = internalEvent, ItemIndex = itemIndex };
            messageBus.Notify(evnt);
        }

        public static void Notify_ListChangedContents(this IMessageBus messageBus, int itemIndex)
        {
            Notify_ListChangedContents(messageBus, itemIndex, null);
        }

        public static void Notify_ListChangedContents(this IMessageBus messageBus)
        {
            Notify_ListChangedContents(messageBus, -1, null);
        }

        public static void Notify_ListItemHighlight_ByID(this IMessageBus messageBus, int itemId)
        {
            var evnt = new NEListItemHighlight { Item = itemId, HighlightType = NEListItemHighlight.HighlightId };
            messageBus.Notify(evnt);
        }

        public static void Notify_ListItemHighlight_ByIndex(this IMessageBus messageBus, int itemIndex)
        {
            var evnt = new NEListItemHighlight { Item = itemIndex, HighlightType = NEListItemHighlight.HighlightIndex };
            messageBus.Notify(evnt);
        }
    }
}

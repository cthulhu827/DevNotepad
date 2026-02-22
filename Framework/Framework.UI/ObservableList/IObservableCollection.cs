using System.Collections.Generic;

namespace Framework.AppInfrastructure
{
    public interface IObservableCollection<out T>
    {
        IMessageBus MessageBus { get; }
        int Count { get; }
        T this[int index] { get; }
        IEnumerable<T> Items { get; }
    }
}
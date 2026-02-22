using System;
using System.Collections.Generic;
using System.Reflection;

namespace Framework.MVC
{
    public class ModelProperties<T>
    {
        private readonly object[] owners;

        protected bool suppressRaiseChangedInSetter;

        public ModelProperties(params object[] owners)
        {
            this.owners = owners;
        }

        public object this[T enumValue]
        {
            get
            {
                var pair = GetPropertyInfo(enumValue);
                return pair.Item1.GetValue(pair.Item2, null);
            }
            set
            {
                var pair = GetPropertyInfo(enumValue);
                pair.Item1.SetValue(pair.Item2, value, null);
                if (!suppressRaiseChangedInSetter)
                {
                    RaiseChanged(enumValue);
                }
            }
        }

        public event EventHandler<EventArgs<IEnumerable<T>>> Changed;

        /// <summary>
        /// Генерирует событие "Модель изменена"
        /// </summary>
        /// <remarks>
        /// При использовании в <see cref="ReactiveMvcController{TChange}"/> приводит к обновлению View.
        /// Можно использовать для обновления View при изменении readonly-свойств (например, для
        /// вычисляемых значений, которые не задаются явно через сеттер, но должны обновиться во View).
        /// </remarks>
        public void RaiseChanged(params T[] changes)
        {
            if (Changed != null)
            {
                Changed(this, new EventArgs<IEnumerable<T>>(changes));
            }
        }

        private Tuple<PropertyInfo, object> GetPropertyInfo(T enumValue)
        {
            foreach (var owner in owners)
            {
                var property = owner.GetType().GetProperty(enumValue.ToString());
                if (property != null)
                {
                    return new Tuple<PropertyInfo, object>(property, owner);
                }
            }

            throw new UnsupportedEnumValueException<T>(enumValue);
        }
    }
}
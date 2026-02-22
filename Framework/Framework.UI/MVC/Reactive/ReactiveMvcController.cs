using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.MVC
{
    public class ReactiveMvcController<TChange> : IReactiveMvcController
        where TChange : struct, IConvertible
    {
        #region Fields

        private readonly EventSuppressor modelSuppressor = new EventSuppressor();

        private ViewProperties<TChange>? viewProperties;

        private ModelProperties<TChange>? modelProperties;

        private object? model;

        #endregion

        #region Methods

        private void SetModelProperties(ModelProperties<TChange>? newModelProperties)
        {
            if (modelProperties != null)
            {
                modelProperties.Changed -= ModelChanged;
                DoModelChanged(null, true);
            }

            modelProperties = newModelProperties;

            if (modelProperties != null)
            {
                DoModelChanged(null);
                modelProperties.Changed += ModelChanged;
            }
        }

        private void SetViewProperties(ViewProperties<TChange>? newViewProperties)
        {
            if (viewProperties != null)
            {
                viewProperties.Changed -= ViewChanged;
            }

            viewProperties = newViewProperties;

            if (viewProperties != null)
            {
                viewProperties.Changed += ViewChanged;
            }
        }

        private void ModelChanged(object sender, EventArgs<IEnumerable<TChange>> e)
        {
            DoModelChanged(e.Value.ToArray());
        }

        private void ViewChanged(object sender, EventArgs<IEnumerable<TChange>> e)
        {
            if (modelProperties == null || viewProperties == null || modelSuppressor.Suppress)
            {
                return;
            }

            InEventRaiserIfExists(() =>
                {
                    var enumValues = e.Value;
                    foreach (var enumValue in enumValues)
                    {
                        modelProperties[enumValue] = viewProperties[enumValue];
                    }
                }, e.Value);
        }

        private void InEventRaiserIfExists(Action action, IEnumerable<TChange> change)
        {
            var provider = model as IProvider<EventRaiser<TChange>>;
            var eventRaiser = provider == null ? null : provider.Get();
            if (eventRaiser != null)
            {
                eventRaiser.Raise(action, change.ToArray());
            }
            else
            {
                action();
            }
        }

        /// <summary>
        /// Применяет к вьюхи изменения в модели
        /// </summary>
        /// <param name="changes">Изменённые свойства в модели. Если <c>null</c>, то все свойства</param>
        /// <param name="reset">
        /// Если <c>true</c>, то модель обнулена, и нужно обнулить вьюху, т.е. для свойств установить
        /// значения <c>null</c> (будет вызываться property.SetValue(..., null), что для value-типов
        /// будет присваивать default(T), т.е. тоже будет работать корректно).
        /// </param>
        private void DoModelChanged(TChange[]? changes, bool reset = false)
        {
            if (modelProperties == null || viewProperties == null)
            {
                return;
            }

            if (changes == null)
            {
                changes = Enums.Values<TChange>().ToArray();
            }

            modelSuppressor.Exec(() =>
                {
                    foreach (var change in changes)
                    {
                        viewProperties[change] = reset ? null : modelProperties[change];
                    }
                });
        }

        #endregion

        #region IReactiveMvcController implementation

        void IReactiveMvcController.SetModel(object newModel)
        {
            model = newModel;
            var provider = newModel as IProvider<ModelProperties<TChange>>;
            SetModelProperties(provider == null ? null : provider.Get());
        }

        void IReactiveMvcController.SetView(object newView)
        {
            var provider = newView as IProvider<ViewProperties<TChange>>;
            SetViewProperties(provider == null ? null : provider.Get());
        }

        #endregion
    }
}
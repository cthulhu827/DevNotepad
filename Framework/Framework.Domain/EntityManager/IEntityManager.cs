using System;
using System.Collections.Generic;

namespace Framework.Domain
{
    public interface IEntityManager
    {
        IEnumerable<T> All<T>() where T : class;
        IEnumerable<VM> All<T, VM>(IViewModelMapper<T, VM> mapper) where T : class;

        T? Get<T>(Func<T, bool> predicate) where T : Entity;

        [Obsolete("Use Add method after save to DB")]
        T Create<T>() where T : Entity, new();

        void AddOrUpdate(Entity entity);
        void Delete(Entity entity);
        void Reinit();
    }
}
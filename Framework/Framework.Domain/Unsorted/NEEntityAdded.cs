using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.AppInfrastructure;

namespace Framework.Domain
{
    public class NEEntityAdded<T> : NotifyEvent where T : Entity
    {
        public T Entity { get; private set; }

        public NEEntityAdded(T entity)
        {
            Entity = entity;
        }
    }
}

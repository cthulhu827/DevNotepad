using System;
using System.Collections.Generic;

namespace Framework.Domain
{
    public interface IGetAllRepository
    {
        IEnumerable<T> GetAll<T>() where T : Entity;
        IEnumerable<object> GetAll(Type type);
    }
}
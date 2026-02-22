using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Domain
{
    public interface IGeneralRepository
    {
        T? Get<T>(int id) where T : Entity;

        void Save(object entity);
        void Delete(object entity);
    }
}

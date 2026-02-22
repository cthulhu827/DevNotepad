using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();
        void Commit();
    }
}

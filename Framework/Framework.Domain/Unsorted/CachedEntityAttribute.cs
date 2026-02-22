using System;

namespace Framework.Domain
{
    public class CachedEntityAttribute : Attribute
    {
        public CachedEntityAttribute(Type repository = null)
        {
            RepositoryInterface = repository;
        }

        public Type RepositoryInterface { get; private set; }
    }
}
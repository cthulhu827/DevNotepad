using Framework.AppInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Domain
{
    public class RepositoryFactory
    {
        private static TypeMapper map = new TypeMapper(true);

        public static T Create<T>(IUnitOfWork unitOfWork)
        {
            return Create<T>(typeof(T), unitOfWork);
        }

        public static T Create<T>(Type repositoryInterface, IUnitOfWork unitOfWork)
        {
            var result = UnitTests.IsTestEnvironment
                ? IoC.Resolve(repositoryInterface)
                : Activator.CreateInstance(map.Get(repositoryInterface), unitOfWork);
            return (T)result;
        }

        public static void Register<TItf, TImpl>()
        {
            map.Register<TItf, TImpl>();
        }

        public static void Register(Type tItf, Type tImpl)
        {
            map.Register(tItf, tImpl);
        }

        public static void Reset()
        {
            map = new TypeMapper(true);
        }
    }
}

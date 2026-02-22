using System;

namespace Framework.Domain
{
    public static class EntityManagerHelper
    {
        public static T? Get<T>(this IEntityManager entityManager, int entityId) where T : Entity
        {
            return entityManager.Get<T>(entity => entity.Id == entityId);
        }

        public static T GetUnsafe<T>(this IEntityManager entityManager, int entityId) where T : Entity
        {
            var result = entityManager.Get<T>(entityId);
            if (result == null)
            {
                throw new BusinessLogicException("{0} with Id = {1} not found in EM", typeof(T).Name, entityId);
            }
            return result;
        }

        public static T GetUnsafe<T>(this IEntityManager entityManager, Func<T, bool> predicate) where T : Entity
        {
            var result = entityManager.Get(predicate);
            if (result == null)
            {
                throw new BusinessLogicException("{0} not found in EM", typeof(T).Name);
            }
            return result;
        }
    }
}
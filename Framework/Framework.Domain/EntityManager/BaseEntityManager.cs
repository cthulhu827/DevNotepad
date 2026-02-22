using System;
using System.Collections.Generic;
using System.Linq;
using Framework.AppInfrastructure;
using Framework.Date;

namespace Framework.Domain
{
    [Singleton, DefaultImplementation(typeof(IEntityManager))]
    public class BaseEntityManager : IEntityManager
    {
        private readonly IDictionary<Type, IList<Entity>> entitiesByTypes = new Dictionary<Type, IList<Entity>>();

        public BaseEntityManager()
        {
            Init();
        }

        #region IEntityManager implementation

        public IEnumerable<T> All<T>() where T : class
        {
            return All(typeof(T)).Cast<T>();
        }

        public IEnumerable<VM> All<T, VM>(IViewModelMapper<T, VM> mapper) where T : class
        {
            return All<T>().Select(mapper.Map);
        }

        public T Get<T>(Func<T, bool> predicate) where T : Entity
        {
            return All<T>().FirstOrDefault(predicate);
        }

        public T Create<T>() where T : Entity, new()
        {
            using (var uow = IoC.Resolve<IUnitOfWork>())
            {
                using (var idRepository = RepositoryFactory.Create<IIdRepository>(uow))
                {
                    var result = new T { Id = idRepository.GetNextId() };
                    AddOrUpdate(result);
                    return result;
                }
            }
        }

        public void AddOrUpdate(Entity entity)
        {
            if (!entity.GetType().HasAttr<CachedEntityAttribute>())
            {
                return;
            }

            var entityList = entitiesByTypes[entity.GetType()];
            if (entityList.Contains(entity))
            {
                entityList.Remove(entity);
            }
            entityList.Add(entity);
        }

        public void Delete(Entity entity)
        {
            entitiesByTypes[entity.GetType()].Remove(entity);
        }

        public virtual void Reinit()
        {
            entitiesByTypes.Clear();

            Init();
        }

        #endregion

        protected virtual IGetAllRepository GetRepositoryForType(IUnitOfWork unitOfWork, Type entityType)
        {
            CachedEntityAttribute attr;
            return entityType.HasAttr(out attr) && attr.RepositoryInterface != null
                       ? RepositoryFactory.Create<IGetAllRepository>(attr.RepositoryInterface, unitOfWork)
                       : RepositoryFactory.Create<IGetAllRepository>(unitOfWork);
        }

        protected virtual IEnumerable<Type> TypesToLoad()
        {
            return AnnotatedClasses.GetTypesWith<CachedEntityAttribute>();
        }

        protected IEnumerable<Entity> All(Type entityType)
        {
            return entitiesByTypes[entityType];
        }

        private void Init()
        {
            using (var uow = IoC.Resolve<IUnitOfWork>())
            {
                foreach (var type in TypesToLoad())
                {
                    LoadEntitiesOfType(type, uow);
                }
            }

            //***Log.Debug("All entities are loaded");
        }

        private void LoadEntitiesOfType(Type type, IUnitOfWork unitOfWork)
        {
            var repository = GetRepositoryForType(unitOfWork, type);

            var now = IoC.Resolve<INow>();
            var startTime = now.GetNow();
            //***Log.Debug("Loading entities of type {0}... ", type.Name);

            var entities = repository.GetAll(type).Cast<Entity>().ToList();
            entitiesByTypes.Add(type, entities);

            var delta = (now.GetNow() - startTime).TotalSeconds;
            //***Log.Debug("Loaded: {0} entiti(es) of type {1} in {2:f3} sec", entities.Count, type.Name, delta);
        }
    }
}
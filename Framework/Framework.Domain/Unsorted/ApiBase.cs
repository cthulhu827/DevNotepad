using System;
using System.Collections.Generic;
using System.Linq;
using Framework.AppInfrastructure;

namespace Framework.Domain
{
    public class ApiBase<E, VM> where E : class
    {
        protected readonly IViewModelMapper<E, VM> mapper;

        public ApiBase(IViewModelMapper<E, VM> mapper)
        {
            this.mapper = mapper;
        }

        public void Save(object entity, IUnitOfWork externalUow = null)
        {
            SaveCollection(entity.AsEnumerable(), externalUow);
        }

        public void SaveCollection(IEnumerable<object> entities, IUnitOfWork externalUow = null)
        {
            var entityCollection = entities.ToArray();
            if (entityCollection.IsEmpty())
            {
                return;
            }

            Wrap(externalUow, uow => DoSaveCollectionToDb(entityCollection, uow));

            // Обновляем em только если метод сам создаёт себе uow, т.е. не вызывается
            // в рамках внешнего. При вызове в рамках внешнего em должен обновляться
            // внешним кодом после успешной записи в базу.
            // note: Пока это всё равно не работает, т.к. сущности берутся напрямую из
            // em (без копирования), поэтому изменения в сущности всё равно будут сразу
            // видны в em, даже если явного вызова em.Save не происходило.
            if (externalUow == null)
            {
                DoSaveCollectionToEm(entityCollection);
            }
        }

        public void Delete(Entity entity, IUnitOfWork externalUow = null, Type repositoryType = null)
        {
            DeleteCollection(entity.AsEnumerable(), externalUow, repositoryType);
        }

        public void DeleteCollection(IEnumerable<Entity> entities, IUnitOfWork externalUow = null, Type repositoryType = null)
        {
            var entityCollection = entities.ToArray();
            if (entityCollection.IsEmpty())
            {
                return;
            }

            Wrap(externalUow, uow => DoDeleteCollectionFromDb(entityCollection, uow, repositoryType));

            // Обновляем em только если метод сам создаёт себе uow, т.е. не вызывается
            // в рамках внешнего. При вызове в рамках внешнего em должен обновляться
            // внешним кодом после успешной записи в базу.
            // note: Пока это всё равно не работает, т.к. сущности берутся напрямую из
            // em (без копирования), поэтому изменения в сущности всё равно будут сразу
            // видны в em, даже если явного вызова em.Save не происходило.
            if (externalUow == null)
            {
                DoDeleteCollectionFromEm(entityCollection);
            }
        }

        public IEnumerable<VM> GetAll()
        {
            return em.All(mapper).ToArray();
        }

        protected IEntityManager em
        {
            get { return IoC.Resolve<IEntityManager>(); }
        }

        protected virtual void DoSaveCollectionToDb(ICollection<object> entityCollection, IUnitOfWork uow)
        {
            var newEntities = entityCollection
                .OfType<Entity>()
                .Where(entity => entity.Id == Entity.NullId)
                .ToArray();
            if (newEntities.Any())
            {
                using (var idRepository = RepositoryFactory.Create<IIdRepository>(uow))
                {
                    foreach (var entity in newEntities)
                    {
                        entity.Id = idRepository.GetNextId();
                    }
                }
            }

            var repository = RepositoryFactory.Create<IGeneralRepository>(uow);
            entityCollection.ForEach(repository.Save);
        }

        protected virtual void DoDeleteCollectionFromDb(ICollection<object> entityCollection,
            IUnitOfWork uow, Type? repositoryType)
        {
            var type = repositoryType ?? typeof(IGeneralRepository);
            var repository = RepositoryFactory.Create<IGeneralRepository>(type, uow);
            entityCollection.ForEach(repository.Delete);
        }

        protected virtual void DoSaveCollectionToEm(ICollection<object> entityCollection)
        {
            entityCollection
                .OfType<Entity>()
                .ForEach(em.AddOrUpdate);
        }

        protected virtual void DoDeleteCollectionFromEm(ICollection<Entity> entityCollection)
        {
            entityCollection.ForEach(entity => em.Delete(entity));
        }

        private static void Wrap(IUnitOfWork? externalUow, Action<IUnitOfWork> action)
        {
            if (externalUow != null)
            {
                action(externalUow);
            }
            else
                using (var uow = IoC.Resolve<IUnitOfWork>())
                {
                    uow.BeginTransaction();
                    action(uow);
                    uow.Commit();
                }
        }
    }
}
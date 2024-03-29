﻿namespace Workers.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataLayer;

    /// <summary>
    /// Represents a service which works with Workers store
    /// </summary>
    public sealed class WorkerService : LifeCycleWorkerService, IWorkersService
    {
        /// <summary>
        /// Repository of workers
        /// </summary>
        private readonly IRepository<WorkerEntity> _repository;

        /// <summary>
        /// Initializes a new instance of the Worker class
        /// </summary>
        /// <param name="repository">Repository of workers</param>
        /// <exception cref="ArgumentNullException">repository is null</exception>
        public WorkerService(IRepository<WorkerEntity> repository)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            _repository = repository;
        }

        /// <summary>
        /// Returns a list of Workers from the store
        /// </summary>
        /// <returns>A list of Workers from the store</returns>
        public IEnumerable<IWorker> GetWorkers()
        {
            foreach (var e in _repository.GetEntities())
            {
                yield return MapToWorker(e);
            }
        }

        /// <summary>
        /// Created an instance of IWorker type
        /// </summary>
        /// <returns>A new instance of IWorker type</returns>
        public IWorker CreateNew()
        {
            return new Worker(this) { Id = Worker.InvalidId, Birthday = DateTime.Now };
        }

        /// <summary>
        /// Updates or adds the new item
        /// </summary>
        /// <param name="worker">Item to save</param>
        /// <param name="isNew">A value which indicating that current worker is new or not</param>
        protected override void SaveInternal(Worker worker, bool isNew)
        {
            WorkerEntity entity;
            if (isNew)
            {
                entity = new WorkerEntity();
                MapToWorkerEntity(worker, entity);
                _repository.Add(entity);

                return;
            }

            entity = GetEntityById(worker.Id);
            if (entity != null)
            {
                MapToWorkerEntity(worker, entity);
            }
            else
            {
                throw new ArgumentException(Localization.strNotFoundWorker);
            }

            _repository.Save(entity);
        }

        /// <summary>
        /// Removes this instance from store
        /// </summary>
        /// <param name="item">Item to delete</param>
        protected override void DeleteInternal(Worker worker)
        {
            var entity = GetEntityById(worker.Id);
            if (entity == null)
            {
                throw new ArgumentException(Localization.strNotFoundWorker);
            }

            _repository.Delete(entity);
        }

        /// <summary>
        /// Rollbacks all changes of current item
        /// </summary>
        /// <param name="item">Item to rollback</param>
        protected override void RollbackInternal(Worker worker)
        {
            var entity = GetEntityById(worker.Id);
            if (entity == null)
            {
                throw new ArgumentException(Localization.strNotFoundWorker);
            }

            MapToWorker(worker, entity);
        }

        /// <summary>
        /// Returns the next vacant Id
        /// </summary>
        /// <returns>The vacant Id</returns>
        protected override int GetVacantId()
        {
            try
            {
                var maxId = _repository.GetEntities().Max(e => e.Id);
                return (int)maxId + 1;
            }
            catch (InvalidOperationException)
            {
                return 0;
            }
        }

        /// <summary>
        /// Maps source entity to target worker
        /// </summary>
        /// <param name="worker">Target worker</param>
        /// <param name="entity">Source entity</param>
        private void MapToWorker(Worker worker, WorkerEntity entity)
        {
            worker.Id = (int)entity.Id;
            worker.Name = entity.Name;
            worker.Surname = entity.Surname;
            worker.Birthday = entity.Birthday;
            worker.Sex = (Sex)entity.Sex;
            worker.HasChildren = Convert.ToBoolean(entity.HasChildren);
        }

        /// <summary>
        /// Maps source entity to target worker
        /// </summary>
        /// <param name="entity">Source entity</param>
        /// <returns>Target worker</returns>
        private Worker MapToWorker(WorkerEntity entity)
        {
            var worker = new Worker(this);
            MapToWorker(worker, entity);
            return worker;
        }

        /// <summary>
        /// Maps source worker to target entity
        /// </summary>
        /// <param name="worker">Source worker</param>
        /// <returns>Target entity</returns>
        private void MapToWorkerEntity(Worker worker, WorkerEntity entity)
        {
            entity.Id = worker.Id;
            entity.Name = worker.Name;
            entity.Surname = worker.Surname;
            entity.Birthday = worker.Birthday;
            entity.Sex = (long)worker.Sex;
            entity.HasChildren = Convert.ToInt64(worker.HasChildren);
        }

        private WorkerEntity GetEntityById(int id)
        {
            return _repository.GetEntities().SingleOrDefault(e => e.Id == id);
        }
    }
}

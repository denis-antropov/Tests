namespace Workers.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataLayer;
    using Interfaces;

    public class WorkerService : LifeCycleWorkerService, IWorkersService
    {
        private readonly IRepository<WorkerEntity> _repository;
        private readonly List<IWorker> _workers;

        public WorkerService(IRepository<WorkerEntity> repository)
        {
            if (repository == null) throw new ArgumentNullException("repository");

            _repository = repository;
            _workers = new List<IWorker>();
        }

        public IEnumerable<IWorker> GetWorkers()
        {
            if(!_workers.Any())
            {
                _workers.AddRange(_repository.GetEntities().Select(MapToWorker));
            }

            return _workers;
        }

        public IWorker CreateNew()
        {
            return new Worker(this) { Id = Worker.InvalidId };
        }

        protected override void SaveInternal(IWorker worker, bool isNew)
        {
            if(isNew)
            {
                _workers.Add(worker);
            }

            _repository.Save(MapToWorkerEntity(worker));
        }

        protected override long GetLastId()
        {
            var worker = GetWorkers().OrderByDescending(e => e.Id).FirstOrDefault();
            if (worker == null)
            {
                return 0;
            }

            return worker.Id;
        }

        private IWorker MapToWorker(WorkerEntity entity)
        {
            return new Worker(this)
            {
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname,
                Birthday = entity.Birthday,
                Sex = (Sex)entity.Sex,
                HasChildren = Convert.ToBoolean(entity.HasChildren)
            };
        }

        private WorkerEntity MapToWorkerEntity(IWorker entity)
        {
            return new WorkerEntity
            {
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname,
                Birthday = entity.Birthday,
                Sex = (long)entity.Sex,
                HasChildren = Convert.ToInt64(entity.HasChildren)
            };
        }
    }
}

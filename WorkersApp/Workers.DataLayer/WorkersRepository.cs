namespace Workers.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class WorkersRepository : IRepository<WorkerEntity>, IDisposable
    {
        private readonly WorkersDbContext _ctx;

        public WorkersRepository()
        {
            _ctx = new WorkersDbContext();
        }

        public IEnumerable<WorkerEntity> GetEntities()
        {
            return _ctx.Workers;
        }

        public WorkerEntity Get(long id)
        {
            return _ctx.Workers.FirstOrDefault(x => x.Id == id);
        }

        public void Add(WorkerEntity entity)
        {
            _ctx.Workers.Add(entity);
            _ctx.SaveChanges();
        }

        public void Save(WorkerEntity entity)
        {
            _ctx.SaveChanges();
        }

        public void Delete(WorkerEntity entity)
        {
            _ctx.Workers.Remove(entity);
            _ctx.SaveChanges();
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}

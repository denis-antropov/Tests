namespace Workers.DataLayer
{
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Represents an implementation of IRepository interface for WorkerEntity class
    /// </summary>
    public class WorkersRepository : IRepository<WorkerEntity>, IDisposable
    {
        /// <summary>
        /// Database context
        /// </summary>
        private readonly WorkersDbContext _ctx;

        /// <summary>
        /// Initializes a new instance of the WorkersRepository class
        /// </summary>
        public WorkersRepository(string repositoryPath)
        {
            _ctx = new WorkersDbContext(repositoryPath);
            _ctx.Database.EnsureCreated();
            _ctx.Database.Migrate();
        }

        /// <summary>
        /// Returns the list of entities
        /// </summary>
        /// <returns>The list of entities</returns>
        public IEnumerable<WorkerEntity> GetEntities()
        {
            return _ctx.Workers;
        }

        /// <summary>
        /// Adds entity to repository
        /// </summary>
        /// <param name="entity">New entity</param>
        public void Add(WorkerEntity entity)
        {
            _ctx.Workers.Add(entity);
            _ctx.SaveChanges();
        }

        /// <summary>
        /// Saves modified entity
        /// </summary>
        /// <param name="entity">Modified entity</param>
        public void Save(WorkerEntity entity)
        {
            _ctx.SaveChanges();
        }

        /// <summary>
        /// Deletes entity from repository
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        public void Delete(WorkerEntity entity)
        {
            _ctx.Workers.Remove(entity);
            _ctx.SaveChanges();
        }

        /// <summary>
        /// Disposes database context
        /// </summary>
        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}
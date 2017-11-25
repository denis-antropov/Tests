namespace Workers.DataLayer
{
    using System.Collections.Generic;
    using Xamarin.Forms;

    public class WorkersDataAccess : IRepository<WorkerEntity>
    {
        private readonly SQLite.SQLiteConnection _database;

        /// <summary>
        /// Initializes a new instance of the WorkersRepository class
        /// </summary>
        public WorkersDataAccess()
        {
            _database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            _database.CreateTable<WorkerEntity>();
        }

        /// <summary>
        /// Returns the list of entities
        /// </summary>
        /// <returns>The list of entities</returns>
        public IEnumerable<WorkerEntity> GetEntities()
        {
            return _database.Table<WorkerEntity>();
        }

        /// <summary>
        /// Adds entity to repository
        /// </summary>
        /// <param name="entity">New entity</param>
        public void Add(WorkerEntity entity)
        {
            _database.Insert(entity);
        }

        /// <summary>
        /// Saves modified entity
        /// </summary>
        /// <param name="entity">Modified entity</param>
        public void Save(WorkerEntity entity)
        {
            if(_database.Table<WorkerEntity>().FirstOrDefault(w => w.Id == entity.Id) == null)
            {
                _database.Insert(entity);
            }
            else
            {
                _database.Update(entity);
            }
        }

        /// <summary>
        /// Deletes entity from repository
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        public void Delete(WorkerEntity entity)
        {
            _database.Delete(entity);
        }

        /// <summary>
        /// Disposes database context
        /// </summary>
        public void Dispose()
        {
            _database.Dispose();
        }
    }
}

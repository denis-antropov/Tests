namespace Workers.DataLayer
{
    using System.Linq;

    /// <summary>
    /// Represents a repository of specific entity
    /// </summary>
    /// <typeparam name="T">Type of entity</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Returns the list of entities
        /// </summary>
        /// <returns>The list of entities</returns>
        IQueryable<T> GetEntities();

        /// <summary>
        /// Saves modified entity
        /// </summary>
        /// <param name="entity">Modified entity</param>
        void Save(T entity);

        /// <summary>
        /// Adds entity to repository
        /// </summary>
        /// <param name="entity">New entity</param>
        void Add(T entity);

        /// <summary>
        /// Deletes entity from repository
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        void Delete(T entity);
    }
}

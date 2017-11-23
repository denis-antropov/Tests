namespace Workers.BusinessLogic
{
    /// <summary>
    /// Represents an interface which manages the domain object in store
    /// </summary>
    /// <typeparam name="T">Domain object type</typeparam>
    public interface ILifeCycleService<in T>
    {
        /// <summary>
        /// Updates or adds the new item
        /// </summary>
        /// <param name="item">Item to save</param>
        void Save(T item);

        /// <summary>
        /// Rollbacks all changes of current item
        /// </summary>
        /// <param name="item">Item to rollback</param>
        void Rollback(T item);

        /// <summary>
        /// Removes this instance from store
        /// </summary>
        /// <param name="item">Item to delete</param>
        void Delete(T item);
    }
}

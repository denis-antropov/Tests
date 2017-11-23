namespace Workers.BusinessLogic
{
    using System;

    /// <summary>
    /// Represents base implementation of ILifeCycleService interface
    /// </summary>
    public abstract class LifeCycleWorkerService : ILifeCycleService<Worker>
    {
        /// <summary>
        /// Updates or adds the new item
        /// </summary>
        /// <param name="item">Item to save</param>
        public void Save(Worker item)
        {
            bool isNew = item.IsNew;
            if (isNew)
            {
                item.Id = GetVacantId();
            }

            SaveInternal(item, isNew);
        }

        /// <summary>
        /// Rollbacks all changes of current item
        /// </summary>
        /// <param name="item">Item to rollback</param>
        public void Rollback(Worker item)
        {
            if (item.IsNew)
                throw new ArgumentException(Localization.strWorkerIsNotInStore);

            RollbackInternal(item);
        }

        /// <summary>
        /// Removes this instance from store
        /// </summary>
        /// <param name="item">Item to delete</param>
        public void Delete(Worker item)
        {
            if (item.IsNew)
                throw new ArgumentException(Localization.strWorkerIsNotInStore);

            DeleteInternal(item);
        }

        /// <summary>
        /// Returns the next vacant Id
        /// </summary>
        /// <returns>The vacant Id</returns>
        protected abstract int GetVacantId();

        /// <summary>
        /// Updates or adds the new item
        /// </summary>
        /// <param name="worker">Item to save</param>
        /// <param name="isNew">A value which indicating that current worker is new or not</param>
        protected abstract void SaveInternal(Worker worker, bool isNew);

        /// <summary>
        /// Removes this instance from store
        /// </summary>
        /// <param name="item">Item to delete</param>
        protected abstract void DeleteInternal(Worker worker);

        /// <summary>
        /// Rollbacks all changes of current item
        /// </summary>
        /// <param name="item">Item to rollback</param>
        protected abstract void RollbackInternal(Worker worker);
    }
}

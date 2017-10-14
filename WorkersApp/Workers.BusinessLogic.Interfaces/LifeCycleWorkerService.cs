namespace Workers.BusinessLogic.Interfaces
{
    using System;

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
                item.Id = GetLastId() + 1;
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
        }

        /// <summary>
        /// Removes this instance from store
        /// </summary>
        /// <param name="item">Item to delete</param>
        public void Delete(Worker item)
        {
            if (item.IsNew)
                throw new ArgumentException(Localization.strWorkerIsNotInStore);
        }

        protected abstract long GetLastId();
        protected abstract void SaveInternal(IWorker worker, bool isNew);
    }
}

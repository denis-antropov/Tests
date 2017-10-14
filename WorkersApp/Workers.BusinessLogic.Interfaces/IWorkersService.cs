namespace Workers.BusinessLogic.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents an interface which returns a list of Workers from the store
    /// </summary>
    public interface IWorkersService
    {
        /// <summary>
        /// Returns a list of Workers from the store
        /// </summary>
        /// <returns>A list of Workers from the store</returns>
        IEnumerable<IWorker> GetWorkers();

        /// <summary>
        /// Created an instance of IWorker type
        /// </summary>
        /// <returns>A new instance of IWorker type</returns>
        IWorker CreateNew();
    }
}

namespace Workers.ViewModels.Interfaces
{
    using System;
    using Workers.BusinessLogic;

    public interface IWorkerItem
    {
        /// <summary>
        /// Gets the worker instance
        /// </summary>
        IWorker Worker { get; }

        /// <summary>
        /// Gets the Id of worker
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Gets the name of worker
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the surname of worker
        /// </summary>
        string Surname { get; }

        /// <summary>
        /// Gets the Birthday of worker
        /// </summary>
        DateTime Birthday { get; }

        /// <summary>
        /// Gets the Sex of worker
        /// </summary>
        Sex Sex { get; }

        /// <summary>
        /// Gets a value indicating whether the worker has children
        /// </summary>
        bool HasChildren { get; }

        /// <summary>
        /// Notifies about properties changes
        /// </summary>
        void Refresh();
    }
}
namespace Workers.BusinessLogic
{
    using System;

    /// <summary>
    /// Represents the base interface of worker
    /// </summary>
    public interface IWorker
    {
        /// <summary>
        /// Gets a value which indicating that current worker is new or not
        /// </summary>
        bool IsNew { get; }

        /// <summary>
        /// Gets the uniq identifier
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Gets or sets the surname of worker
        /// </summary>
        /// <exception cref="ArgumentException">Surname cannot be null or empty</exception>
        string Surname { get; set; }

        /// <summary>
        /// Gets or sets the name of worker
        /// </summary>
        /// <exception cref="ArgumentException">Name cannot be null or empty</exception>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the Birthday of worker
        /// </summary>
        DateTime Birthday { get; set; }

        /// <summary>
        /// Gets or sets the Sex of worker
        /// </summary>
        Sex Sex { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the worker has children
        /// </summary>
        bool HasChildren { get; set; }

        /// <summary>
        /// Saves changes or add it as new object to store
        /// </summary>
        void Save();

        /// <summary>
        /// Rollbacks all changes
        /// </summary>
        /// <exception cref="InvalidOperationException">Worker is not in the store</exception>
        void Rollback();

        /// <summary>
        /// Removes this instance from store
        /// </summary>
        /// <exception cref="InvalidOperationException">Worker is not in the store</exception>
        void Delete();
    }
}

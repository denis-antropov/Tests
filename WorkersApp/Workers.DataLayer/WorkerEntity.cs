namespace Workers.DataLayer
{
    using System;

    /// <summary>
    /// Represents a entity for Worker presentation
    /// </summary>
#if Portable
    [SQLite.Table("Workers")]
#else
    [System.ComponentModel.DataAnnotations.Schema.Table("Workers")]
#endif
    public class WorkerEntity
    {
        /// <summary>
        /// Gets or sets the uniq identifier
        /// </summary>
#if Portable
        [SQLite.PrimaryKey]
#endif
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the surname of worker
        /// </summary>
#if Portable
        [SQLite.NotNull]
#endif
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the name of worker
        /// </summary>
#if Portable
        [SQLite.NotNull]
#endif
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Birthday of worker
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// Gets or sets the Sex of worker
        /// </summary>
        public long Sex { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the worker has children
        /// </summary>
        public long HasChildren { get; set; }
    }
}

namespace Workers.DataLayer
{
    using System.Data.Entity;

    /// <summary>
    /// Represents a context for working with Workers database
    /// </summary>
    public class WorkersDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the WorkersDbContext class
        /// </summary>
        internal WorkersDbContext()
            : base("DefaultConnection")
        {
        }

        /// <summary>
        /// Gets or sets the set of workers
        /// </summary>
        public DbSet<WorkerEntity> Workers { get; set; }
    }
}
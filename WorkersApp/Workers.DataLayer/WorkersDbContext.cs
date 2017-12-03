namespace Workers.DataLayer
{
    using System;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Represents a context for working with Workers database
    /// </summary>
    public class WorkersDbContext : DbContext
    {
        private readonly string _dbFilePath;

        /// <summary>
        /// Initializes a new instance of the WorkersDbContext class
        /// </summary>
        internal WorkersDbContext(string dbFilePath)
        {
            _dbFilePath = dbFilePath ?? throw new ArgumentNullException();
        }

        /// <summary>
        /// Gets or sets the set of workers
        /// </summary>
        public DbSet<WorkerEntity> Workers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_dbFilePath}");
        }
    }
}
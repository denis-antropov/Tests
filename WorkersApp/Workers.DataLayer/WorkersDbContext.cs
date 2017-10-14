namespace Workers.DataLayer
{
    using System.Data.Entity;

    public class WorkersDbContext : DbContext
    {
        internal WorkersDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<WorkerEntity> Workers { get; set; }
    }
}


namespace Workers.DataLayer
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Workers")]
    public class WorkerEntity
    {
        public long Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public DateTime Birthday { get; set; }

        public long Sex { get; set; }

        public long HasChildren { get; set; }
    }
}

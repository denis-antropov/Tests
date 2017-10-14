namespace Workers.BusinessLogic.Interfaces
{
    using System;

    /// <summary>
    /// Represents an implementation of IWorker interface
    /// </summary>
    public class Worker : IWorker
    {
        /// <summary>
        /// Id of worker which is not contained in the store
        /// </summary>
        public const long InvalidId = -1;

        /// <summary>
        /// Life cycle service
        /// </summary>
        private readonly ILifeCycleService<Worker> _lifeCycleService;

        /// <summary>
        /// Surname field
        /// </summary>
        private string _surname;

        /// <summary>
        /// Name field
        /// </summary>
        private string _name;

        /// <summary>
        /// Initializes a new instance of the Worker class
        /// </summary>
        /// <param name="lifeCycleService">Life cycle service</param>
        /// <exception cref="ArgumentNullException">lifeCycleService is null</exception>
        public Worker(ILifeCycleService<Worker> lifeCycleService)
        {
            if (lifeCycleService == null) throw new ArgumentNullException("lifeCycleService");

            _lifeCycleService = lifeCycleService;
        }

        /// <summary>
        /// Gets a value which indicating that current worker is new or not
        /// </summary>
        public bool IsNew
        {
            get { return Id == InvalidId; }
        }

        /// <summary>
        /// Gets or sets the uniq identifier
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the surname of worker
        /// </summary>
        public string Surname
        {
            get { return _surname; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Surname");

                _surname = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of worker
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Name");

                _name = value;
            }
        }

        /// <summary>
        /// Gets or sets the Birthday of worker
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// Gets or sets the Sex of worker
        /// </summary>
        public Sex Sex { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the worker has children
        /// </summary>
        public bool HasChildren { get; set; }

        /// <summary>
        /// Saves changes or add it as new object to store
        /// </summary>
        public void Save()
        {
            _lifeCycleService.Save(this);
        }

        /// <summary>
        /// Rollbacks all changes
        /// </summary>
        public void Rollback()
        {
            _lifeCycleService.Rollback(this);
        }

        /// <summary>
        /// Removes this instance from store
        /// </summary>
        public void Delete()
        {
            _lifeCycleService.Delete(this);
        }
    }
}

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
        public const int InvalidId = -1;

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
            if (lifeCycleService == null) throw new ArgumentNullException(nameof(lifeCycleService));

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
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the surname of worker
        /// </summary>
        /// <exception cref="ArgumentException">Surname cannot be null or empty</exception>
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
        /// <exception cref="ArgumentException">Name cannot be null or empty</exception>
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
        /// <exception cref="InvalidOperationException">Worker has invalid state</exception>
        public void Save()
        {
            try
            {
                _lifeCycleService.Save(this);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(Localization.strInvalidWorker, ex);
            }
        }

        /// <summary>
        /// Rollbacks all changes
        /// </summary>
        /// <exception cref="InvalidOperationException">Worker is not in the store</exception>
        public void Rollback()
        {
            try
            {
                _lifeCycleService.Rollback(this);
            }
            catch (ArgumentException ex)
            {
                throw new InvalidOperationException(Localization.strInvalidWorker, ex);
            }
        }

        /// <summary>
        /// Removes this instance from store
        /// </summary>
        /// <exception cref="InvalidOperationException">Worker is not in the store</exception>
        public void Delete()
        {
            try
            {
                _lifeCycleService.Delete(this);
            }
            catch (ArgumentException ex)
            {
                throw new InvalidOperationException(Localization.strInvalidWorker, ex);
            }
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns> A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return Id;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            Worker worker = obj as Worker;
            if (worker == null)
                return false;

            return worker.Id == Id;
        }
    }
}

namespace Workers.ViewModels
{
    using System;
    using Workers.BusinessLogic;
    using Workers.ViewModels.Interfaces;

    /// <summary>
    /// Represents simple view model of worker
    /// </summary>
    public class WorkerItem : ViewModelBase, IWorkerItem
    {
        /// <summary>
        /// Initializes a new instance of the WorkerItem class
        /// </summary>
        /// <param name="worker">Worker instance</param>
        /// <exception cref="ArgumentNullException">worker is null</exception>
        public WorkerItem(IWorker worker)
        {
            if (worker == null) throw new ArgumentNullException(nameof(worker));

            Worker = worker;
        }

        /// <summary>
        /// Gets the worker instance
        /// </summary>
        public IWorker Worker { get; private set; }

        /// <summary>
        /// Gets the Id of worker
        /// </summary>
        public int Id
        {
            get
            {
                return Worker.Id;
            }
        }

        /// <summary>
        /// Gets the name of worker
        /// </summary>
        public string Name
        {
            get
            {
                return Worker.Name;
            }
        }

        /// <summary>
        /// Gets the surname of worker
        /// </summary>
        public string Surname
        {
            get
            {
                return Worker.Surname;
            }
        }

        /// <summary>
        /// Gets the Birthday of worker
        /// </summary>
        public DateTime Birthday
        {
            get
            {
                return Worker.Birthday;
            }
        }

        /// <summary>
        /// Gets the Sex of worker
        /// </summary>
        public Sex Sex
        {
            get
            {
                return Worker.Sex;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the worker has children
        /// </summary>
        public bool HasChildren
        {
            get
            {
                return Worker.HasChildren;
            }
        }

        /// <summary>
        /// Notifies about properties changes
        /// </summary>
        public void Refresh()
        {
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Surname));
            OnPropertyChanged(nameof(Birthday));
            OnPropertyChanged(nameof(Sex));
            OnPropertyChanged(nameof(HasChildren));
        }
    }
}
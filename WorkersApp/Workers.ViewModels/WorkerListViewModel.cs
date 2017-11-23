namespace Workers.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Workers.BusinessLogic;
    using Workers.ViewModels.Common;

    /// <summary>
    /// Represents a view model of workerslist
    /// </summary>
    public class WorkerListViewModel : ViewModelBase
    {
        /// <summary>
        /// Worker service
        /// </summary>
        private readonly IWorkersService _workersService;

        /// <summary>
        /// Worker modifier
        /// </summary>
        private readonly IWorkerModifier _workerModifier;

        /// <summary>
        /// List of workers
        /// </summary>
        private ObservableCollection<WorkerItem> _workers;

        /// <summary>
        /// The seletected worker item
        /// </summary>
        private WorkerItem _selcectedWorker;

        /// <summary>
        /// Initializes a new instance of the WorkerItem class
        /// </summary>
        /// <param name="workersService">Worker service</param>
        /// <param name="workerModifier">Worker modifier</param>
        /// <exception cref="ArgumentNullException">workersService or workerModifier is null</exception>
        public WorkerListViewModel(IWorkersService workersService, IWorkerModifier workerModifier)
        {
            if (workersService == null)
                throw new ArgumentNullException(nameof(workersService));

            if (workerModifier == null)
                throw new ArgumentNullException(nameof(workerModifier));

            _workersService = workersService;
            _workerModifier = workerModifier;
            DeleteWorkerCommand = new RelayCommand(() => DeleteWorker(), () => SelectedWorker != null);
            EditWorkerCommand = new RelayCommand(() => EditWorker(), () => SelectedWorker != null);
            CreateWorkerCommand = new RelayCommand(() => CreateWorker());
            Workers.GetType();
        }

        /// <summary>
        /// Gets the list of workers
        /// </summary>
        public ObservableCollection<WorkerItem> Workers
        {
            get
            {
                if (_workers == null)
                {
                    _workers = new ObservableCollection<WorkerItem>();
                    foreach (var w in _workersService.GetWorkers())
                    {
                        _workers.Add(new WorkerItem(w));
                    }
                }

                return _workers;
            }
        }

        /// <summary>
        /// Gets or sets the seletected worker item
        /// </summary>
        public WorkerItem SelectedWorker
        {
            get { return _selcectedWorker; }
            set
            {
                _selcectedWorker = value;
                OnPropertyChanged();
            }
        }


        /// <summary>
        /// Gets the delete worker command
        /// </summary>
        public ICommand DeleteWorkerCommand { get; set; }

        /// <summary>
        /// Gets the create worker command
        /// </summary>
        public ICommand CreateWorkerCommand { get; set; }

        /// <summary>
        /// Gets the edit worker command
        /// </summary>
        public ICommand EditWorkerCommand { get; set; }

        /// <summary>
        /// Deletes the selected worker
        /// </summary>
        private void DeleteWorker()
        {
            SelectedWorker.Worker.Delete();
            Workers.Remove(SelectedWorker);
            SelectedWorker = null;
        }

        /// <summary>
        /// Create a new worker
        /// </summary>
        private void CreateWorker()
        {
            var newWorker = _workersService.CreateNew();
            if(_workerModifier.Modify(newWorker))
            {
                var newWorkerItem = new WorkerItem(newWorker);
                Workers.Add(newWorkerItem);
                SelectedWorker = newWorkerItem;
            }
        }

        /// <summary>
        /// Edits the selected worker
        /// </summary>
        private void EditWorker()
        {
            if (_workerModifier.Modify(SelectedWorker.Worker))
            {
                SelectedWorker.Refrech();
            }
        }
    }
}

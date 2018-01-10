namespace Workers.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Workers.BusinessLogic;
    using Workers.ViewModels.Interfaces;
    using z3r0.Utils;
    using z3r0.Utils.Extensions;
    using z3r0.Utils.ViewModels.Commands;
    using z3r0.Utils.ViewModels.Commands.Async;

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
        private readonly IWorkerItemFactory _workerItemFactory;
        private readonly IUserInteraction _userInteraction;

        /// <summary>
        /// List of workers
        /// </summary>
        private ObservableCollection<IWorkerItem> _workers;

        /// <summary>
        /// The seletected worker item
        /// </summary>
        private IWorkerItem _selcectedWorker;

        /// <summary>
        /// Initializes a new instance of the IWorkerItem class
        /// </summary>
        /// <param name="workersService">Worker service</param>
        /// <param name="workerModifier">Worker modifier</param>
        /// <exception cref="ArgumentNullException">workersService or workerModifier is null</exception>
        public WorkerListViewModel(IWorkersService workersService, IWorkerModifier workerModifier, 
            IWorkerItemFactory workerItemFactory, IUserInteraction userInteraction)
        {
            _workersService = workersService ?? throw new ArgumentNullException(nameof(workersService));
            _workerModifier = workerModifier ?? throw new ArgumentNullException(nameof(workerModifier));
            _workerItemFactory = workerItemFactory ?? throw new ArgumentNullException(nameof(workerItemFactory));
            _userInteraction = userInteraction ?? throw new ArgumentNullException(nameof(userInteraction));

            DeleteWorkerCommand = new AsyncRelayCommand(() => DeleteWorker(), () => SelectedWorker != null);
            EditWorkerCommand = new RelayCommand(() => EditWorker(), () => SelectedWorker != null);
            CreateWorkerCommand = new RelayCommand(() => CreateWorker());
            Workers.GetType();
        }

        /// <summary>
        /// Gets the list of workers
        /// </summary>
        public ObservableCollection<IWorkerItem> Workers
        {
            get
            {
                if (_workers == null)
                {
                    _workers = new ObservableCollection<IWorkerItem>();
                    foreach (var w in _workersService.GetWorkers())
                    {
                        _workers.Add(_workerItemFactory.Create(w));
                    }
                }

                return _workers;
            }
        }

        /// <summary>
        /// Gets or sets the seletected worker item
        /// </summary>
        public IWorkerItem SelectedWorker
        {
            get { return _selcectedWorker; }
            set
            {
                if (_selcectedWorker != value)
                {
                    _selcectedWorker = value;
                    OnPropertyChanged();
                }
            }
        }
        
        /// <summary>
        /// Gets the delete worker command
        /// </summary>
        public AsyncRelayCommand DeleteWorkerCommand { get; private set; }

        /// <summary>
        /// Gets the create worker command
        /// </summary>
        public RaisableCommand CreateWorkerCommand { get; private set; }

        /// <summary>
        /// Gets the edit worker command
        /// </summary>
        public RaisableCommand EditWorkerCommand { get; private set; }

        /// <summary>
        /// Deletes the selected worker
        /// </summary>
        private async Task DeleteWorker()
        {
            var result = await _userInteraction.NotifyQuestionAsync(
                Localization.strQuestionToDeleteWorker.AsFormat(SelectedWorker.Worker.Name), 
                UserOptions.YesNo);
            if (result == UserAnswer.Yes)
            {
                var workerToDelete = _selcectedWorker;
                SelectedWorker = null;

                workerToDelete.Worker.Delete();
                Workers.Remove(workerToDelete);
            }
        }

        /// <summary>
        /// Create a new worker
        /// </summary>
        private void CreateWorker()
        {
            var newWorker = _workersService.CreateNew();
            _workerModifier.ModificationFinished += CreatingFinished;

            _workerModifier.Modify(newWorker);
        }

        /// <summary>
        /// Edits the selected worker
        /// </summary>
        private void EditWorker()
        {
            _workerModifier.ModificationFinished += EditingFinished;

            _workerModifier.Modify(SelectedWorker.Worker);
        }

        /// <summary>
        /// Notifies about changed property
        /// </summary>
        /// <param name="propertyName">Name of changed property</param>
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            DeleteWorkerCommand.RaiseCanExecuteChanged();
            EditWorkerCommand.RaiseCanExecuteChanged();
        }

        private void EditingFinished(object sender, ModificationStateEventArgs e)
        {
            _workerModifier.ModificationFinished -= EditingFinished;

            if (e.State)
                SelectedWorker.Refresh();
        }

        private void CreatingFinished(object sender, ModificationStateEventArgs e)
        {
            _workerModifier.ModificationFinished -= CreatingFinished;

            if (e.State)
            {
                var newIWorkerItem = _workerItemFactory.Create(e.Worker);
                Workers.Add(newIWorkerItem);
                SelectedWorker = newIWorkerItem;
            }
        }
    }
}

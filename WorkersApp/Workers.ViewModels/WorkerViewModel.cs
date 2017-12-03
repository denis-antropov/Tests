namespace Workers.ViewModels
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using Common;
    using System.Linq;
    using Workers.BusinessLogic;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Represents view model of worker
    /// </summary>
    public class WorkerViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        /// <summary>
        /// Worker instance
        /// </summary>
        private readonly IWorker _worker;

        /// <summary>
        /// The name of worker
        /// </summary>
        private string _name;

        /// <summary>
        /// The surname of worker
        /// </summary>
        private string _surname;

        /// <summary>
        /// Occurs when the validation errors have changed for a property or for the entire
        /// entity.
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        /// Initializes a new instance of the WorkerViewModel class
        /// </summary>
        /// <param name="worker">Worker instance</param>
        /// <exception cref="ArgumentNullException">worker is null</exception>
        public WorkerViewModel(IWorker worker)
        {
            if (worker == null) throw new ArgumentNullException(nameof(worker));

            _worker = worker;
            SaveCommand = new RelayCommand(() => Save(), Validate);
            CancelCommand = new RelayCommand(() => Rollback());
            _name = worker.Name;
            _surname = worker.Surname;
        }

        /// <summary>
        /// Gets the save command
        /// </summary>
        public RelayCommand SaveCommand { get; private set; }

        /// <summary>
        /// Gets cancel command
        /// </summary>
        public RelayCommand CancelCommand { get; private set; }

        /// <summary>
        /// Gets or sets the name of worker
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }
       
        /// <summary>
        /// Gets or sets the surname of worker
        /// </summary>
        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                if (_surname != value)
                {
                    _surname = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the Birthday of worker
        /// </summary>
        public DateTime Birthday
        {
            get
            {
                return _worker.Birthday;
            }
            set
            {
                if (Birthday != value)
                {
                    _worker.Birthday = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the Sex of worker
        /// </summary>
        public Sex Sex
        {
            get
            {
                return _worker.Sex;
            }
            set
            {
                if (Sex != value)
                {
                    _worker.Sex = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the worker has children
        /// </summary>
        public bool HasChildren
        {
            get
            {
                return _worker.HasChildren;
            }
            set
            {
                if (HasChildren != value)
                {
                    _worker.HasChildren = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the entity has validation errors.
        /// </summary>
        public bool HasErrors => GetErrors(nameof(Name)).Cast<string>().Any() || GetErrors(nameof(Surname)).Cast<string>().Any();

        /// <summary>
        /// Validates view model
        /// </summary>
        /// <returns>True, if no error; otherwise - false</returns>
        private bool Validate()
        {
            return !HasErrors;
        }

        /// <summary>
        /// Updates or adds the new item in the store
        /// </summary>
        private void Save()
        {
            _worker.Name = _name;
            _worker.Surname = _surname;
            _worker.Save();
        }

        /// <summary>
        /// Rollbacks all changes of this worker
        /// </summary>
        private void Rollback()
        {
            if (!_worker.IsNew)
            {
                _worker.Rollback();
                _name = _worker.Name;
                _surname = _worker.Surname;
            }
        }

        /// <summary>
        /// Gets the validation errors for a specified property or for the entire entity.
        /// </summary>
        /// <param name="propertyName">
        /// The name of the property to retrieve validation errors for; or null or System.String.Empty,
        /// to retrieve entity-level errors.
        /// </param>
        /// <returns>The validation errors for the property or entity.</returns>
        public IEnumerable GetErrors(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(Name):
                    if (string.IsNullOrWhiteSpace(Name))
                        return new string[] { Localization.strNameIsEmpty };
                    break;
                case nameof(Surname):
                    if (string.IsNullOrWhiteSpace(Surname))
                        return new string[] { Localization.strSurnameIsEmpty };
                    break;
            }

            return new string[0];
        }

        /// <summary>
        /// Notifies about changed property
        /// </summary>
        /// <param name="propertyName">Name of changed property</param>
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            SaveCommand.RaiseCanExecuteChanged();
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(Surname)));
        }
    }
}
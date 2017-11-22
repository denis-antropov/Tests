﻿namespace Workers.ViewModels
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Windows.Input;
    using BusinessLogic.Interfaces;
    using Common;
    using System.Linq;

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
        public ICommand SaveCommand { get; private set; }

        /// <summary>
        /// Gets cancel command
        /// </summary>
        public ICommand CancelCommand { get; private set; }

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
                    ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(Name)));
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
                    ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(Surname)));
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
    }
}
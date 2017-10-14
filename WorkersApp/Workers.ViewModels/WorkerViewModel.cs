﻿namespace Workers.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Windows.Input;
    using BusinessLogic.Interfaces;
    using Common;

    /// <summary>
    /// Represents view model of worker
    /// </summary>
    public class WorkerViewModel : ViewModelBase, IDataErrorInfo
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
        ///  Gets an error message indicating what is wrong with this object.
        /// </summary>
        public string Error
        {
            get { return string.Empty; }
        }

        /// <summary>
        /// Gets the error message for the property with the given name.
        /// </summary>
        /// <param name="columnName">The name of the property whose error message to get.</param>
        /// <returns>The error message for the property. The default is an empty string ("").</returns>
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                  case nameof(Name):
                    if (string.IsNullOrWhiteSpace(Name))
                        return Localization.strNameIsEmpty;
                    break;
                  case nameof(Surname):
                    if (string.IsNullOrWhiteSpace(Surname))
                        return Localization.strSurnameIsEmpty;
                    break; 
                }

                return string.Empty;
            }
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
        public bool Sex
        {
            get
            {
                return Convert.ToBoolean((int)_worker.Sex);
            }
            set
            {
                if (Sex != value)
                {
                    _worker.Sex = (Sex)Convert.ToInt32(value);
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
        /// Validates view model
        /// </summary>
        /// <returns>True, if no error; otherwise - false</returns>
        private bool Validate()
        {
            if (this[nameof(Name)] == string.Empty &&
                this[nameof(Surname)] == string.Empty)
            {
                return true;
            }

            return false;
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
            _worker.Rollback();
            _name = _worker.Name;
            _surname = _worker.Surname;
        }
    }
}
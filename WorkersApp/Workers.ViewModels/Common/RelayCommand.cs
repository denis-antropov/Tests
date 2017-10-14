namespace Workers.ViewModels.Common
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// A command whose sole purpose is to relay its functionality to other
    /// objects by invoking delegates. The default return value for the CanExecute
    /// method is 'true'.
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// Command useful action
        /// </summary>
        private readonly Action _execute;

        /// <summary>
        /// Defines the way that determines whether the command can execute in its current state.
        /// </summary>
        private readonly Func<bool> _canExecute;

        /// <summary>
        /// Defines the way to handle the occurred exception. If exception is handled, returns true; otherwise - false
        /// </summary>
        private readonly Func<Exception, bool> _exceptionHandler;

        /// <summary>
        /// Initializes a new instance of the RelayCommand class. 
        /// Creates a new command that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommand(Action execute)
            : this(execute, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RelayCommand class. 
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        /// <exception cref="ArgumentNullException">Parameter execute is null</exception>
        public RelayCommand(Action execute, Func<bool> canExecute)
            : this(execute, canExecute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RelayCommand class. 
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="exceptionHandler">Handles the exception</param>
        /// <exception cref="ArgumentNullException">Parameter execute is null</exception>
        public RelayCommand(Action execute, Func<Exception, bool> exceptionHandler) :
            this(execute, null, exceptionHandler)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RelayCommand class. 
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        /// <param name="exceptionHandler">Handles the exception</param>
        /// <exception cref="ArgumentNullException">Parameter execute is null</exception>
        public RelayCommand(Action execute, Func<bool> canExecute, Func<Exception, bool> exceptionHandler)
        {
            if (execute == null) throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
            _exceptionHandler = exceptionHandler;
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        /// <param name="parameter">Data used by the command.  
        /// If the command does not require data to be passed, this object can be set to null.</param>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        /// <exception cref="System.InvalidOperationException">CanExecute is false</exception>
        public void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                // The method can not be called
                throw new InvalidOperationException(Localization.strMethodCannotBeCalled);
            }

            try
            {
                _execute();
            }
            catch (Exception ex)
            {
                // verifies if ExceptionHandler is nulll
                // or tries to handle Exception
                // throws all exceptions to user
                if (_exceptionHandler == null || !_exceptionHandler(ex))
                {
                    throw;
                }
            }
        }
    }
}
